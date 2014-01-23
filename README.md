# [![iPayy](https://portal.ipayy.com/v001/images/logo.png)](https://www.ipayy.com) Windows Store App 8/8.1 Demo

## Overview
Missing revenue from customers without credit card or customers not willing to share credit card details?

[iPayy](https://www.ipayy.com) is the solution for you!

## Features

### MicroPayment
This module enables you, as a merchant, to accept payment for your digital goods ranging anywhere between ₹1 and ₹500.

### 1 Step Payment
Just one step for customer to pay for your goods. They need to just enter OTP sent on their mobile and click on Confirm and its done!

### Seamless Integration
Its very very very easy to complete the integration for this module. Refer to [installation & usage instruction](#installation--usage)

### Multiple Mobile Carriers
iPayy is available across multiple mobile carriers, including Idea and Airtel. The service will be extended to a few more operators, increasing the coverage to 80% GSM base in India.

### Automatic Discovery of Details
iPayy tries to automatically discover the customer's mobile number and operator to enable seamless payment to complete the payment process in just 1 step. In case, customer is not on mobile network, he needs to enter his mobile number and select his operator. 

##Installation & Usage
###Import iPayy SDK
In Visual Studio, right click your project and click Manage NuGet Packages. In the search box, enter iPayy, select iPayy from the results and click install.

Make sure you have NuGet Package Manager 2.1 or newer installed in Visual Studio. Update if needed (Tools -> Extensions and Updates)

###Setting up the SDK
####Prerequisites for Windows 8.1 Applications
**Important:** Before you start using the SDK on Windows 8.1 projects, please make sure that you add http://\*.ipayy.com to the "Content URIs" section in the **Package.appxmanifest** file. This is required to allow the payment website to communicate with the SDK. The SDK will not work correctly if this rule is not added. This is only needed for applications targeted for Windows 8.1.

###Using the SDK
####Pre-requisites before you start coding :-
* A Merchant Key and an Application Key for your application
* Some White-listed numbers for testing
* The necessary project references imported

####Create Payment object
```c#
using iPayy;
Payment payment = new Payment();
```
####Populate required properties of the object
```c#
payment.MerchantKey = "<Your Merchant Key";
payment.ApplicationKey = "<Your Application Key>";
payment.CurrencyCode = "INR";
payment.ItemCode = "<Item Code>";
payment.ItemName = "<Item Name>";
payment.ItemPrice = "<Item Price>";
payment.Msisdn = "<Mobile number of user>"; // Optional
payment.RequestToken = "<Request token id>"; //Or
payment.RequestToken = payment.generateUniqueRequestId(); 
// Msisdn is optional. This helps iPayy to prefill the text boxes.
```
####Subscribe to the PaymentCompletedEventHandler event
```c#
payment.PaymentCompletedEventHandler += payment_PaymentCompletedEventHandler;
```
####Call the Charge method to initiate the payment
```c#
payment.Charge()
```
####Receive the payment result
```c#
private void payment_PaymentCompletedEventHandler(Payment payment, PaymentCompletedEventArgs eventArgs)
{
	if (eventArgs.PaymentStatus == PaymentStatus.Success)
	{
		//Handle success payment;
	}
	else
	{
		//Handle payment failed;
	}
}
```
PaymentCompletedEventArgs has the following properties which can help you understand more about the payment:
<table>
	<tr>
		<th>Property Name</th><th>Description</th>
	</tr>
	<tr>
		<td>PaymentStatus</td><td>Current status of the payment.<br />Pending, Success or Failure</td>
	</tr>
	<tr>
		<td>AmountCharged</td><td>Actual amount charged.</td>
	</tr>
	<tr>
		<td>CustomerCode</td><td>Unique customer code (per customer) from iPayy</td>
	</tr>
	<tr>
		<td>RequestToken</td><td>Request token id</td>
	</tr>
	<tr>
		<td>TransactionDate</td><td>Transaction timestamp (in milliseconds)</td>
	</tr>
	<tr>
		<td>TransactionId</td><td>Transaction Id from iPayy</td>
	</tr>
	<tr>
		<td>FailureReason</td><td>Reason for failure: None UserCancel SessionTimeOut TooManyFailures LowBalance InvalidOperatorSelected Fraud NetworkFailure Other</td>
	</tr>
	<tr>
		<td>ErrorCode</td><td>Error Code as returned from iPayy Server. Refer error codes document.</td>
	</tr>
	<tr>
		<td>ErrorMessage</td><td>Verbose error message mapping to error code</td>
	</tr>
</table>
