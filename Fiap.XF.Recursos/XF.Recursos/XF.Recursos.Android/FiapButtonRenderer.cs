using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Recursos.CustomControl;
using XF.Recursos.Droid;
using static Android.Widget.TextView;

[assembly: ExportRenderer(typeof(FiapButton), typeof(FiapButtonRenderer))]
namespace XF.Recursos.Droid
{
    class FiapButtonRenderer : ButtonRenderer
    {
        public FiapButtonRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            FiapButton btn = (FiapButton)Element;

            Rect ret = new Rect();
            GetDrawingRect(ret);
            Paint paint = new Paint();

            canvas.DrawText(btn.Texto, 0, 0, paint);          

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Gray);
            }
        }
    }
}
