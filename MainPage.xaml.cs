using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace rpi_win10_blinking_leds
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GpioPin _yellowLED;
        private GpioPin _redLED;
        private GpioPin _greenLED;
        private GpioPin _blueLED;

        private DispatcherTimer _timer;
        private int _loop = 0;

        public MainPage()
        {
            this.InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(250);
            _timer.Tick += BlinkLeds;
            _timer.Start();

            SetupLeds();
        }

        private void SetupLeds()
        {
            var gpio = GpioController.GetDefault();

            _redLED = gpio.OpenPin(0);
            _redLED.SetDriveMode(GpioPinDriveMode.Output);

            _yellowLED = gpio.OpenPin(5);
            _yellowLED.SetDriveMode(GpioPinDriveMode.Output);

            _greenLED = gpio.OpenPin(6);
            _greenLED.SetDriveMode(GpioPinDriveMode.Output);

            _blueLED = gpio.OpenPin(13);
            _blueLED.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void On(GpioPin pin)
        {
            pin.Write(GpioPinValue.High);
        }

        private void Off(GpioPin pin)
        {
            pin.Write(GpioPinValue.Low);
        }

        private void BlinkLeds(object o, object e)
        {
            switch (_loop)
            {
                case 1:
                    On(_greenLED);
                    break;
                case 2:
                    On(_blueLED);
                    break;
                case 3:
                    On(_redLED);
                    break;
                case 4:
                    On(_yellowLED);
                    break;
                default:
                    Off(_greenLED);
                    Off(_blueLED);
                    Off(_yellowLED);
                    Off(_redLED);
                    break;
            }

            _loop = _loop >= 4 ? 0 : _loop + 1;
        }
    }
}
