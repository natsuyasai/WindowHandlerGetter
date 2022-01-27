    public class WindowHandlerGeter
    {
        private readonly string _rootWindowTitle;
        private readonly string _targetWindowTitle;
        private IntPtr _targetWindowHandler = IntPtr.Zero;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rootWindowTitle">検索対象のウィンドウが存在するルート画面のタイトル</param>
        /// <param name="targetWindowStr">検索対象のウィンドウの</param>
        public WindowHandlerGeter(string rootWindowTitle, string targetWindowTitle)
        {
            _rootWindowTitle = rootWindowTitle;
            _targetWindowTitle = targetWindowTitle;
        }

        /// <summary>
        /// ウィンドウハンドラ取得
        /// </summary>
        /// <returns></returns>
        public IntPtr GetWindowHandler()
        {
            IntPtr rootWndHandle = NativeMethods.FindWindow(null, _rootWindowTitle);

            if(rootWndHandle == null)
            {
                return IntPtr.Zero;
            }

            NativeMethods.EnumChildWindows(rootWndHandle, EnumChildWindowCallBack, IntPtr.Zero);

            return _targetWindowHandler;
        }

        /// <summary>
        /// 子ウィンドウ列挙コールバック
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        private bool EnumChildWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            StringBuilder windowText = new StringBuilder(512);
            int ret = NativeMethods.GetWindowText(hWnd, windowText, windowText.Capacity);
            if (windowText.ToString().Contains(_targetWindowTitle))
            {
                _targetWindowHandler = hWnd;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
