using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class MenuList : UserControl
{
    [Category("动态菜单"), Description("菜单集合"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<MenuItem> List { get; set; }
    [Category("动态菜单"), Description("菜单大小")]
    public Size MenuSize { get; set; }
    [Category("动态菜单"), Description("内部间距")]
    public Padding ContentPadding { get; set; }
    [Category("动态菜单"), Description("横向间距")]
    public int HorizontalSpace { get; set; }
    [Category("动态菜单"), Description("垂直间距")]
    public int VerticalSpace { get; set; }
    [Category("动态菜单"), Description("默认图片")]
    public Image DefaultImage { get; set; }

    private Point currPoint = Point.Empty;

    public MenuList()
    {
        this.List = new List<MenuItem>();
        this.ContentPadding = new Padding();
    }

    bool needPaint = true;

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        if (needPaint)
            base.OnPaintBackground(e);
    }

    public void RefreshLayout()
    {
        this.Controls.Clear();
        this.currPoint.X = this.ContentPadding.Left;
        this.currPoint.Y = this.ContentPadding.Top;
        //this.List.Sort((c1, c2) =>
        //{
        //    //return    c1.DisplayOrder - c2.DisplayOrder;
        //    return 0;
        //});
        foreach (var menu in this.List)
        {
            MenuControl control = new MenuControl();
            control.Size = this.MenuSize;
            control.Location = this.currPoint;
            if (menu.Image == null)
                control.Image = this.DefaultImage;
            else
                control.Image = menu.Image;
            control.Text = menu.Text;
            control.Name = menu.Id;
            menu.Control = control;
            this.Controls.Add(control);
            this.currPoint.X += this.HorizontalSpace + this.MenuSize.Width;
            if (this.Width - this.currPoint.X - control.Width - this.ContentPadding.Right < 0)
            {
                this.currPoint.X = this.ContentPadding.Left;
                this.currPoint.Y += this.VerticalSpace + this.MenuSize.Height;
            }
        }
        this.Height = this.currPoint.Y + this.MenuSize.Height + this.ContentPadding.Bottom;
        needPaint = false;
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // MenuList
        // 
        this.Name = "MenuList";
        this.Size = new System.Drawing.Size(221, 194);
        this.ResumeLayout(false);

    }
}

public class MenuItem
{
    public MenuItem()
    {
    }

    public int DisplayOrder { get; set; }
    public string Text { get; set; }
    public string Id { get; set; }
    public Image Image { get; set; }
    public Control Control { get; set; }
}

internal class MenuControl : Control
{
    public MenuControl()
    {
    }

    public Image Image { get; set; }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (!this.Focused)
            this.Focus();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        //this.Text = "got";
        this.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        //this.Text = "lost";
        this.Invalidate();
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        //base.OnPaintBackground(pevent);
        Graphics g = pevent.Graphics;
        if (this.Image != null)
            g.DrawImage(this.Image, new Rectangle(Point.Empty, this.Size),
                0, 0, this.Image.Width, this.Image.Height, GraphicsUnit.Pixel);
        SizeF sText = g.MeasureString(this.Text, this.Font);
        g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor),
           (this.Width - sText.Width) / 2, (this.Height - sText.Height) / 2);
    }
}