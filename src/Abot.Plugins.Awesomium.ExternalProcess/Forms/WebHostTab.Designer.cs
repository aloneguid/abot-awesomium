namespace Abot.Plugins.Awesomium.ExternalProcess.Forms
{
   partial class WebHostTab
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         global::Awesomium.Core.WebPreferences webPreferences1 = new global::Awesomium.Core.WebPreferences(true);
         this.WebBrowserControl = new global::Awesomium.Windows.Forms.WebControl(this.components);
         this.AddressText = new System.Windows.Forms.TextBox();
         this.WebSessionProvider = new global::Awesomium.Windows.Forms.WebSessionProvider(this.components);
         this.SuspendLayout();
         // 
         // WebBrowserControl
         // 
         this.WebBrowserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.WebBrowserControl.Location = new System.Drawing.Point(4, 30);
         this.WebBrowserControl.Size = new System.Drawing.Size(613, 378);
         this.WebBrowserControl.TabIndex = 0;
         this.WebBrowserControl.DocumentReady += new global::Awesomium.Core.DocumentReadyEventHandler(this.Awesomium_Windows_Forms_WebControl_DocumentReady);
         this.WebBrowserControl.LoadingFrameFailed += new global::Awesomium.Core.LoadingFrameFailedEventHandler(this.Awesomium_Windows_Forms_WebControl_LoadingFrameFailed);
         this.WebBrowserControl.LoadingFrameComplete += new global::Awesomium.Core.FrameEventHandler(this.Awesomium_Windows_Forms_WebControl_LoadingFrameComplete);
         // 
         // AddressText
         // 
         this.AddressText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.AddressText.Location = new System.Drawing.Point(4, 4);
         this.AddressText.Name = "AddressText";
         this.AddressText.Size = new System.Drawing.Size(613, 20);
         this.AddressText.TabIndex = 1;
         // 
         // WebSessionProvider
         // 
         webPreferences1.AllowInsecureContent = false;
         webPreferences1.CanScriptsCloseWindows = false;
         webPreferences1.CanScriptsOpenWindows = false;
         webPreferences1.JavascriptViewChangeSource = false;
         webPreferences1.JavascriptViewEvents = false;
         webPreferences1.JavascriptViewExecute = false;
         webPreferences1.LoadImagesAutomatically = false;
         webPreferences1.PdfJS = false;
         webPreferences1.Plugins = false;
         webPreferences1.RemoteFonts = false;
         webPreferences1.WebAudio = false;
         this.WebSessionProvider.Preferences = webPreferences1;
         this.WebSessionProvider.Views.Add(this.WebBrowserControl);
         // 
         // WebHostTab
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.AddressText);
         this.Controls.Add(this.WebBrowserControl);
         this.Name = "WebHostTab";
         this.Size = new System.Drawing.Size(620, 411);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox AddressText;
      private global::Awesomium.Windows.Forms.WebControl WebBrowserControl;
      private global::Awesomium.Windows.Forms.WebSessionProvider WebSessionProvider;
   }
}
