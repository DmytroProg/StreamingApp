using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StreamingApp.WPF;

/// <summary>
/// Interaction logic for DrawWindow.xaml
/// </summary>
/// 

enum ShapeType
{
    Rect,
    Circle,
}

public partial class DrawWindow : Window
{
    private const double CanvasOpacity = .3;

    private Shape? currentShape;
    private ShapeType shapeType;
    private bool isDrawing;
    private Point startPoint;

    public DrawWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        popup.IsOpen = true;
        popup.HorizontalOffset = (Width - popup.Width) / 2;
    }

    private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        currentShape = GetShape();
        if (currentShape is null) return;

        SetupShape();
        isDrawing = true;
        canvas.Children.Add(currentShape);
        startPoint = e.GetPosition(canvas);
        Canvas.SetLeft(currentShape, startPoint.X);
        Canvas.SetTop(currentShape, startPoint.Y);
    }

    private void canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDrawing) return;
        
        PlaceShape(e.GetPosition(canvas));
    }

    private void PlaceShape(Point point)
    {
        if(currentShape is null) return;

        currentShape.Width = Math.Abs(point.X - startPoint.X);
        if (point.X < startPoint.X)
        {
            Canvas.SetLeft(currentShape, point.X);
        }

        currentShape.Height = Math.Abs(point.Y - startPoint.Y);
        if (point.Y < startPoint.Y)
        {
            Canvas.SetTop(currentShape, point.Y);
        }
    }

    private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        isDrawing = false;
        currentShape = null;
    }

    private void rectBtn_Click(object sender, RoutedEventArgs e)
    {
        shapeType = ShapeType.Rect;
        canvas.Opacity = CanvasOpacity;
    }

    private void circleBtn_Click(object sender, RoutedEventArgs e)
    {
        shapeType = ShapeType.Circle;
        canvas.Opacity = CanvasOpacity;
    }

    private void clearBtn_Click(object sender, RoutedEventArgs e)
    {
        canvas.Children.Clear();
    }
    
    private void cancelBtn_Click(object sender, RoutedEventArgs e)
    {
        canvas.Opacity = 0;
    }

    private void SetupShape()
    {
        if(currentShape is null) return;

        currentShape.Stroke = Brushes.Red;
        currentShape.StrokeThickness = 3;
    }

    private Shape? GetShape() => shapeType switch
    {
        ShapeType.Rect => new Rectangle(),
        ShapeType.Circle => new Ellipse(),
        _ => null,
    };
}