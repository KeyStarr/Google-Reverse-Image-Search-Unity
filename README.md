# Google-Reverse-Image-Search-Unity
## What's new?
Well, time passed and i finally got to the github, so now i can complete some people's request to publish my solution on parsing google's best guess html code without the help of the external server.

Also there are two significant editions that i made:
- I completely re-wrote the original code in order to structurize it and make it more readable.
- Added the Microsoft Vision API support for the image recognition - now a user can choose which service to use (Google or Microsoft).
- If no info was gotten from the wikipedia (for the description of the recognized object) the app tries to get some from the Oxford Dictionary API.

##What to do to run it
You'll have to obtain your own API keys for the following services:
- [Vuforia](https://library.vuforia.com/articles/Training/Vuforia-License-Manager.html#license-key)
- [Google Search Engine](https://developers.google.com/custom-search/json-api/v1/overview)
- [Microsoft Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/)
- [Oxford Dictionary](https://developer.oxforddictionaries.com/)

The process of obtaining the first two keys was kindly recorded by Matthew [in his video](https://www.youtube.com/watch?v=EP-s2ayECsI&lc=z12si3wp5nudgleyj04chnbajyujctjptkg0k.1518014293682062).