using System;
using OpenTK;
using OpenTK.Audio;

namespace OpenALSample
{
    public class Sound
    {
        #region 動的メンバ
        /// <summary>
        /// ソース、バッファの初期値
        /// </summary>
        static readonly int DEFAULT_SOURCE_VALUE = 0;

        /// <summary>
        /// デバイス
        /// </summary>
        static IntPtr Device { get; set; }

        /// <summary>
        /// コンテキスト
        /// </summary>
        static ContextHandle CurrentContext { get; set; }

        /// <summary>
        /// レンダラー
        /// </summary>
        public static string Renderer { get; private set; }

        /// <summary>
        /// ベンダー
        /// </summary>
        public static string Vendor { get; private set; }

        /// <summary>
        /// バージョン
        /// </summary>
        public static string Version { get; private set; }
        #endregion
        
        /// <summary>
        /// コンストラクタ(インスタンス化禁止)
        /// </summary>
        Sound() { }

        /// <summary>
        /// 初期化
        /// </summary>
        public static bool Initialize()
        {
            try
            {
                // デバイスを開く
                Device = Alc.OpenDevice(null);
                if (Device == null)
                    throw new Exception("OpenALデバイスの作成に失敗しました.");

                // コンテキスト作成
                CurrentContext = Alc.CreateContext(Device, (int[])null);
                if (CurrentContext == null)
                    throw new Exception("OpenALコンテキストの作成に失敗しました.");

                // 作成したコンテキストをカレント設定
                Alc.MakeContextCurrent(CurrentContext);

                // OpenALのバージョン取得
                Version = AL.Get(ALGetString.Version);
                Vendor = AL.Get(ALGetString.Vendor);
                Renderer = AL.Get(ALGetString.Renderer);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 開放
        /// </summary>
        public static void Release()
        {
            // コンテキスト開放
            if (CurrentContext != ContextHandle.Zero)
            {
                Alc.MakeContextCurrent(ContextHandle.Zero);
                Alc.DestroyContext(CurrentContext);
            }

            // デバイスを閉じる
            if (Device != IntPtr.Zero)
                Alc.CloseDevice(Device);

            // 初期化
            CurrentContext = ContextHandle.Zero;
            Device = IntPtr.Zero;
        }
    }
}