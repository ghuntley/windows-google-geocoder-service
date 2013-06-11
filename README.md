# Windows Google Geocoder Service



## Database Models

SourceDb.cs is automatically generated using SQLMetal. To regenerate it install the Microsoft Windows 7 or 8 SDK from Microsoft and run the following commands:

	# cd C:\PROGRA~2\Microsoft SDKs\Windows\v7A.0\Bin
	# sqlmetal /server:localhost /database:interactive /code:C:\tmp\SourceDb.cs /namespace:SourceDb /context:SourceDbDataContext /sprocs
    


## Google Maps Enterprise


### Recommended Reading

* [How to sign requests with our ClientId and generate the signed HMAC using our CrytpoKey]( https://developers.google.com/maps/documentation/business/webservices#digital_signatures).

### Welcome Email

Welcome to Google Maps API for Business, we are pleased to provide you with your Google Maps API for Business:

	Client ID: gme-interactiveptyltd1
	Crypto Key: wLOnVDalfa1OtoyZv-rWAGKKJAg=
	Client ID Type: Internal


Some Maps API for Business client IDs can only be used in particular environments or applications, based on their type. The following link lists the features of each client ID type. Please note that this link can only be accessed after logging into the Google Enterprise Support Portal at http://www.google.com/enterprise/portal. If you need access, please review the section "Google Enterprise Support" below. 

Client ID Types (you will also find this link listed under the Resources section in the support portal): https://c.na4.visual.force.com/apex/ResourceDetail?id=a0Z600000017fqEEAQ

**Support End Date for your Account: Jun 11, 2014**

This welcome letter describes how to enable Google Maps API for Business in your applications and is intended for a technical audience.

#### Maps API for Business vs Free Maps API Usage (required reading)

There are important differences between Google Maps API for Business and the Free Maps APIs. Developers migrating to Maps API for Business from the Free APIs must read this letter in full and understand its content, so that your sites and applications will be able to utilize Google Maps API for Business correctly.

Developers working with the Google Maps APIs for the first time must also be familiar with the information in this message to use the Maps API for Business license and Client ID correctly.

Failure to replace a Free API key with your Maps API for Business Client ID, or failure to use your Client ID in new applications, will void your support contract, negate your purchased quota uplifts, and prevent Maps API for Business API access.

#### Maps API for Business Authorization (required reading)

Using your Client ID to make Google Maps API requests from sites not enabled for your Client ID will fail. You must enable domains that you own to use your Client ID before using the Client ID in your applications.

Your Client ID is currently enabled to make Google Maps API requests from the following domains:
 
	Note that:
	
	Subdomains of registered domains are also enabled (i.e., registering http://site.com also enables http://subdomain1.site.com, http://subdomain2.site.com, etc.).
	If a port number is not specified for a site, any port number can be used (i.e., registering http://site.com also enables http://site.com:80, http://site.com:5521, etc.).
	If a site is accessed over https it must be specified above with a https:// prefix (i.e., http://site.com DOES NOT enable https://site.com).
	Enable Additional Domains
 
Starting on 27-February-2012, you will be able to enable new URLs for your Maps for Business client ID by using a new self-service tool. 

To enable new URLs, you should no longer contact Google Enterprise Support. Instead, please:

* Login to the portal at http://www.google.com/enterprise/portal
* Select the Resource named "Maps: Configure URL", in the left panel. 
* Follow the instructions on this page. 
* After self-enablement, URLs will be ready for use in a few minutes.
* To disable use of your Client ID on particular URLs, please contact Google Enterprise Support by creating a “non-technical” support case for your Client ID.
* Google Maps API for Business resellers cannot enable new URLs for end customers’ Client IDs in the self-service tool. 
* Resellers will need to continue to create support cases for URL additions and deletions.

If you have not received credentials for this portal, please visit the following FAQ for instructions on requesting a portal login:

http://www.google.com/support/enterprise/bin/answer.py?answer=142246#q3
 
For general information on enabling URLs for your Client ID, please visit the documentation here:
http://developers.google.com/maps/documentation/business/guide#URLs


#### URL Signing (required reading)

To use selected Google Maps API Web Services with your Client ID, you will need to generate unique signatures for your requests, using the following cryptographic key:

wLOnVDalfa1OtoyZv-rWAGKKJAg=

This key should only be used to generate URL signatures, as described here:
http://developers.google.com/maps/documentation/business/webservices#digital_signatures

Please ensure that this cryptographic key remains private. You should never include this key in the source or content of any web pages or request URLs, post it to a developer or help forum, or disclose it to anybody outside your organization.

Full documentation on how to sign your request URLs, and the list of Web Services that require URL signing, can be found in the Maps API for Business Developer's Guide.

Notifications

Updates to the Maps APIs may affect your applications. For notification of changes, you should subscribe to the relevant notify Google Groups for the APIs you are using, as described here:

http://developers.google.com/maps/faq#notify

Please take a moment now to subscribe to the -notify groups for APIs that you are using or plan to use.

You may also subscribe to the following RSS feed:

http://google.force.com/services/xml/MapsRSS

to receive updates from the Google Maps API for Business support teams.


Features

Your Google Maps API for Business client ID provides:
Access to the Google Maps API over https for secure websites
Larger static map images (up to 2048 x 2048 pixels)
The assurance of the Google Maps API for Business Service Level Agreement
Access to Google Enterprise Support
HTTP geocoder batch quota of 100,000 addresses per day at 10 per second
Upcoming additional services, to be announced in the -notify groups above

#### Google Enterprise Support (required reading)

Terms and service: Please take a moment now to visit the Google Maps API for Business Technical Support Service Guidelines, which are available at:

http://www.google.com/enterprise/earthmaps/legal/us/maps_tssg.html

The TSSG describe the support terms included with your Google Maps API for Business Service Level Agreement.

Documentation: We encourage you to make the documentation and FAQs your first point of reference for assistance with Google Maps API for Business:
Maps API for Business Documentation: http://developers.google.com/maps/documentation/business
FAQs specific to Google Maps API for Business: http://developers.google.com/maps/documentation/business/faq
Additional FAQs for the Google Maps API: http://developers.google.com/maps/faq
Getting Started guides and troubleshooting articles in the Resources section of the Google Enterprise Support Portal

Contacting support: If you need assistance after consulting the documentation above, you can contact Google Enterprise Support via:

Web: http://www.google.com/enterprise/portal

To file a support case, visit the "Cases" section of the portal.

If you have not received credentials for this portal, please visit the following FAQ:

http://www.google.com/support/enterprise/bin/answer.py?answer=142246#q3

for instructions on requesting a portal login.

Phone: If your live Google Maps production applications ever become inaccessible to your end users for longer than 15 minutes, US customers can call us at:

	1-877-355-5787
 
In EMEA and JAPAC, customers can call +1-404-978-9282, or check the Google Enterprise Support Portal at http://www.google.com/enterprise/portal for local toll-free numbers.
 
When dialing this number to report an outage, select the Geospatial option, and enter the following PIN:
 
	890131415 

This “P1” request hotline is available 24x5 on business days. Please have your client ID, gme-interactiveptyltd1, available when you call.

#### Monitoring Your Maps API Usage

Usage reporting allows you to track the number of page views used by your Client ID. For more details on the definition of a page view, please see the Maps API for Business FAQ.

You can also set a channel id on each application that you build which uses your Client ID, to obtain a breakdown of your page view usage per application built.

You should visit the Resources section of the Support Portal to access your usage reports.

#### Google Groups

The Google Maps API Google Group is an excellent resource where you can interact with our active community of Google Maps API developers:

http://groups.google.com/group/Google-Maps-API

If you are developing applications using the Google Maps API for Flash, we recommend joining the Google Maps API for Flash Google Group:

http://groups.google.com/group/google-maps-api-for-flash

#### Training

The following training resources are also available:

* Google Maps API for Business Support Technical Webinars WebEx presentations held monthly.
* http://geo-announce.appspot.com.

Thank you for choosing Google Maps API for Business!

Sincerely,
The Google Maps API for Business Team