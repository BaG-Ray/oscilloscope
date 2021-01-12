using ComSample.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InteractiveDataDisplay.WPF;
using System.Reactive.Linq;
using System.Windows.Threading;

namespace ComSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mainWindow;

        #region 示波器部分

        LineGraph ChFirst = new LineGraph();
        LineGraph ChSecond = new LineGraph();
        LineGraph ChThird = new LineGraph();
        LineGraph ChFourth = new LineGraph();


        List<Double> TimeValue = new List<Double>();
        List<Double> ChFirstValue = new List<Double>();
        List<Double> ChSecondValue = new List<Double>();
        List<Double> ChThirdValue = new List<Double>();
        List<Double> ChFourthValue = new List<Double>();

        private Double timeTempValue = 0;
        private Double ChFirstTempValue = 0;
        private Double ChSecondTempValue = 0;
        private Double ChThirdTempValue = 0;
        private Double ChFourthTempValue = 0;


        #endregion

        private volatile bool canStop = false; //线程终止
        public bool gridingStatus = false;




        public MainWindow()
        {
            InitializeComponent();
            mainWindow = new MainViewModel();
            this.DataContext = mainWindow;
            InitRegister();

            #region 示波器部分

            lines.Children.Add(ChFirst);
            lines.Children.Add(ChSecond);
            lines.Children.Add(ChThird);
            lines.Children.Add(ChFourth);

            trychart();
            Thread t = new Thread(tem);
            t.Start();

            #endregion

        }

        /// <summary>
        /// 注册类
        /// </summary>
        private void InitRegister()
        {
            Messenger.Default.Register<string>(this, "PlayReciveFlashing", PlayReciveFlashing); //
            Messenger.Default.Register<string>(this, "PlaySendFlashing", PlaySendFlashing); //
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="empty"></param>
        private void Open(string empty)
        {
            mainWindow.CurrentParameter.Open();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="empty"></param>
        private void Click(string empty)
        {
            mainWindow.CurrentParameter.Send();
        }

        /// <summary>
        /// 无边框拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// 清空界发送and缓存区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            mainWindow.CurrentParameter.ClearText();
        }

        #region Animation

        /// <param name="emp"></param>
        private void PlaySendFlashing(string emp)
        {
            var ani = new ColorAnimation();
            ani.Duration = new TimeSpan(1000);
            ani.From = (Color)ColorConverter.ConvertFromString("#4EEE94");
            ani.To = (Color)ColorConverter.ConvertFromString("#EE3B3B");
            BlkSend.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ani);
        }

        private void PlayReciveFlashing(string emp)
        {

            this.Dispatcher.Invoke(() =>
            {
                var ani = new ColorAnimation();
                ani.Duration = new TimeSpan(1000);
                ani.From = (Color)ColorConverter.ConvertFromString("#4EEE94");
                ani.To = (Color)ColorConverter.ConvertFromString("#EE3B3B");
                BlkRecive.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ani);
            });

        }

        #endregion


        #region 示波器部分

        public void trychart()
        {
            ChFirst.Stroke = new SolidColorBrush(Colors.Crimson);
            ChSecond.Stroke = new SolidColorBrush(Colors.Gold);
            ChThird.Stroke = new SolidColorBrush(Colors.Aqua);
            ChFourth.Stroke = new SolidColorBrush(Colors.Aquamarine);


            ChFirst.StrokeThickness = 5;
            ChSecond.StrokeThickness = 5;
            ChThird.StrokeThickness = 5;
            ChFourth.StrokeThickness = 5;
        }

        public void tem() //line graph
        {
            while (true)
            {

                #region 按钮绑定

                Dispatcher.Invoke(
                    new Action(
                        delegate
                        {
                            if (ButtonCHFi.IsChecked == true)
                            {
                                ChFirst.Stroke = new SolidColorBrush(Colors.Crimson);
                                ChFirst.Description = String.Format("CH1:");
                            }
                            else
                            {
                                ChFirst.Stroke = new SolidColorBrush(Colors.Transparent);
                                ChFirst.Description = String.Format("");
                            }

                            if (ButtonCHSe.IsChecked == true)
                            {
                                ChSecond.Stroke = new SolidColorBrush(Colors.Gold);
                                ChSecond.Description = String.Format("CH2:");

                            }
                            else
                            {
                                ChSecond.Stroke = new SolidColorBrush(Colors.Transparent);
                                ChSecond.Description = String.Format("");

                            }

                            if (ButtonCHTh.IsChecked == true)
                            {
                                ChThird.Stroke = new SolidColorBrush(Colors.Aqua);
                                ChThird.Description = String.Format("CH3:");

                            }
                            else
                            {
                                ChThird.Stroke = new SolidColorBrush(Colors.Transparent);
                                ChThird.Description = String.Format("");

                            }

                            if (ButtonCHFo.IsChecked == true)
                            {
                                ChFourth.Stroke = new SolidColorBrush(Colors.Aquamarine);
                                ChFourth.Description = String.Format("CH4:");

                            }
                            else
                            {
                                ChFourth.Stroke = new SolidColorBrush(Colors.Transparent);
                                ChFourth.Description = String.Format("");

                            }
                        }
                    ));



                #endregion


                System.Threading.Thread.Sleep(50);
                TimeValue.Add(timeTempValue);
                ChFirstValue.Add(ChFirstTempValue);
                ChSecondValue.Add(ChSecondTempValue);
                ChThirdValue.Add(ChThirdTempValue);
                ChFourthValue.Add(ChFourthTempValue);

                timeTempValue = timeTempValue + 10;
                ChFirstTempValue = Math.Sin(Math.PI * timeTempValue / 360);
                ChSecondTempValue = Math.Sin(Math.PI * (timeTempValue / 360 + 0.5));
                ChThirdTempValue = Math.Sin(Math.PI * (timeTempValue / 360 + 1));
                ChFourthTempValue = Math.Sin(Math.PI * (timeTempValue / 360 + 1.5));



                Dispatcher.BeginInvoke(new Action(delegate
                {
                    List<Double> listTime = new List<Double>();
                    List<Double> listChFirstValue = new List<Double>();
                    List<Double> listChSecondValue = new List<Double>();
                    List<Double> listChThirdValue = new List<Double>();
                    List<Double> listChFourthValue = new List<Double>();


                    int tempChFirst, tempTime, tempChSecond, tempChThird, tempChFourth;
                    for (tempTime = 0; tempTime < TimeValue.Count(); tempTime++)
                        listTime.Add(TimeValue[tempTime]);
                    for (tempChFirst = 0; tempChFirst < TimeValue.Count(); tempChFirst++)
                        listChFirstValue.Add(ChFirstValue[tempChFirst]);
                    for (tempChSecond = 0; tempChSecond < TimeValue.Count(); tempChSecond++)
                        listChSecondValue.Add(ChSecondValue[tempChSecond]);
                    for (tempChThird = 0; tempChThird < TimeValue.Count(); tempChThird++)
                        listChThirdValue.Add(ChThirdValue[tempChThird]);
                    for (tempChFourth = 0; tempChFourth < TimeValue.Count(); tempChFourth++)
                        listChFourthValue.Add(ChFourthValue[tempChFourth]);


                    #region 精简图像(完成)

                    if (listTime.Last() % 1000 == 0 && listTime.Last() != 0)
                    {
                        chart.PlotOriginX = listTime.Last();

                        //listTime.RemoveRange(0, 100);
                        //listChFirstValue.RemoveRange(0, 100);
                        //listChSecondValue.RemoveRange(0, 100);
                        //listChThirdValue.RemoveRange(0, 100);
                        //listChFourthValue.RemoveRange(0, 100);

                        listTime.Clear();
                        listChFirstValue.Clear();
                        listChSecondValue.Clear();
                        listChThirdValue.Clear();
                        listChFourthValue.Clear();
                    }


                    ChFirst.Plot(listTime, listChFirstValue);
                    ChSecond.Plot(listTime, listChSecondValue);
                    ChThird.Plot(listTime, listChThirdValue);
                    ChFourth.Plot(listTime, listChFourthValue);


                    #endregion

                }
        ));
            }
        }

        #endregion

        private void GridOn_OnChecked(object sender, RoutedEventArgs e)
        {
            if (ButtonGridOn.IsChecked == true)
            {
                ButtonGridOn.Content = "OFF";
            }
            else
            {
                ButtonGridOn.Content = "ON";
            }
        }
    }
}
