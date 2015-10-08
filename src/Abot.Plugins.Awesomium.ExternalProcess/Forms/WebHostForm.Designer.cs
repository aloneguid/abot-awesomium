namespace Abot.Plugins.Awesomium.ExternalProcess.Forms
{
   partial class WebHostForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.WebHostTabOne = new WebHostTab();
         this.SuspendLayout();
         // 
         // WebHostTabOne
         // 
         this.WebHostTabOne.Dock = System.Windows.Forms.DockStyle.Fill;
         this.WebHostTabOne.Location = new System.Drawing.Point(0, 0);
         this.WebHostTabOne.Name = "WebHostTabOne";
         this.WebHostTabOne.Size = new System.Drawing.Size(743, 414);
         this.WebHostTabOne.TabIndex = 0;
         // 
         // WebHostForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(743, 414);
         this.Controls.Add(this.WebHostTabOne);
         this.Name = "WebHostForm";
         this.Text = "Awesomium";
         this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
         this.ResumeLayout(false);

      }

      #endregion

      private WebHostTab WebHostTabOne;
   }
}

