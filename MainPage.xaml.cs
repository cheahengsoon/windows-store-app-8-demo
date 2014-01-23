using iPayy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace iPayyDemo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : iPayyDemo.Common.LayoutAwarePage
    {
        
        private Payment payment;

        public MainPage()
        {
            this.InitializeComponent();
            paymentGrid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Visibility.Collapsed;
            payment = new Payment();
            payment.MerchantKey = "TSo-Pr8LkLy0E497TTyt8Q";
            payment.ApplicationKey = "yChjKjgSRy09oZGRapOrgQ";
            payment.CurrencyCode = "INR";
            payment.ItemCode = "1";
            payment.ItemName = "Demo Payment (Windows Store)";
            payment.ItemPrice = "1";
            //payment.Msisdn = "919164318945";
            payment.RequestToken = payment.generateUniqueRequestId();
            payment.PaymentCompletedEventHandler += payment_PaymentCompletedEventHandler;
            payment.Charge();
        }

        private void payment_PaymentCompletedEventHandler(Payment payment, PaymentCompletedEventArgs eventArgs)
        {
            paymentGrid.Visibility = Visibility.Visible;
            BitmapImage tn = new BitmapImage();
            if (eventArgs.PaymentStatus == PaymentStatus.Success)
            {
                tn.UriSource = new Uri(@"ms-appx:///Assets/success.png");
                txtPaymentStatus.Text = "SUCCESS";
            }
            else
            {
                txtPaymentStatus.Text = "FAILED";
                tn.UriSource = new Uri(@"ms-appx:///Assets/cancel.png");
            }
            txtPaymentExtInfo.Text = "";

            foreach (PropertyInfo info in typeof(PaymentCompletedEventArgs).GetRuntimeProperties())
            {
                object value = info.GetValue(eventArgs, null);
                if (value != null)
                {
                    txtPaymentExtInfo.Text += (info.Name + " = " + value + "\n");
                }
            }
            paymentImage.Source = tn;
        }
    }
}
