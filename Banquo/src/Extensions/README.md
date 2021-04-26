
# Status Implementing CodeceptJS Metods
My goal isn't to make a perfect copy of the CodeceptJS methods, but at least
I thought it would be interesting to keep track of how far along the path I am.


**Bold items** = Implemented, ~~strikethrough~~ = Not planned
## Actions

|   |   |   |   |
|---|---|---|---|
| **AmOnPage** | AppendField | AttachFile | ~~CancelPopup~~ |
| **CheckOption** | ClearCookie | **ClearField** | **Click** |
| ~~CloseCurrentTab~~ | ~~DoubleClick~~ | ~~DragAndDrop~~ | ~~DragSlider~~ |
| ExecuteAsyncScript | ExecuteSript | **FillField** | **ForceClick** |
| ~~ForceRightClick~~ | MoveCursorTo | ~~OpenNewTab~~ | ~~PressKey~~ |
| ~~PressKeyDown~~ | ~~PressKeyUp~~ | **RefreshPage** | ResizeWindow |
| ~~RightClick~~ | RunInWeb | ~~RunOnAndroid~~ | ~~runOnIOS~~ |
| SaveElementScreenshot | SaveScreenshot | ScrollIntoView | ScrollPageBottom |
| ScrollPageTop | ScrollTo | **SelectOption(s)**<br />[ByText,ByValue] |SetCookie |
| ~~SetGeoLocation~~ | ~~SwitchTo~~ | ~~SwitchToNextTab~~ | ~~SwitchToPreviousTab~~ |
| ~~SwitchToWindow~~ | **Type** | **UncheckOption** | UseWebDriverTo |

#### Element Actions

|   |   |   |   |
|---|---|---|---|
| AppendField | **Check(s)Option** | **Clear(s)Field** | **Click** |
| ~~DoubleClick~~ | ~~DragAndDrop~~ | ~~DragSlider~~ | **Fill(s)Field** |
| **ForceClick** | ~~ForceRightClick~~ | ~~RightClick~~ | SaveElementScreenshot |
| ScrollIntoView | ScrollTo | **SelectOption(s)**<br />[ByText,ByValue] | ~~SwitchTo~~ |
| **Type** | **UncheckOption** |  |  |

## Asserts

### Driver Asserts

|   |   |   |   |
|---|---|---|---|
| **DontSee** |DontSeeCheckboxIsChecked|DontSeeCookie | DontSeeCurrentUrlEquals |
| DontSeeElement | DontSeeElementInDOM | DontSeeInCurrentUrl | DontSeeInField |
| DontSeeInSource | **DontSeeInTitle** | SeeAttributesOnElements | SeeCheckboxIsChecked |
| SeeCookie | SeeCssPropertiesOnElements | SeeCurrentUrlEquals | **SeeElement** |
| **SeeElementInDOM** | SeeInCurrentUrl | SeeInField | ~~SeeInPopup~~ |
| SeeInSource | **SeeInTitle** | SeeNumberOfElements | SeeNumberOfVisibleElements |
| SeeTextEquals | **SeeTitleEquals** | **See** | |

### Element Asserts
These will be useful for keeping tests fluent (not part of CodeceptJS; taken from Cypress).

|   |   |   |   |
|---|---|---|---|
| DontSee | DontSeeCheckboxIsChecked | DontSeeInField |  **See** (== Driver <ins>SeeElement</ins>) |
| SeeCheckboxIsChecked | SeeInDOM | SeeInField | SeeTextEquals |
| **SelectOption(s)**<br />[ByText,ByValue] |

## Grabs

*These currently need testing*

|   |   |   |   |
|---|---|---|---|
| ~~GrabAllWindowHandles~~ | **GrabAttributeFrom** | **GrabAttributeFromAll** | ~~GrabBrowserLogs~~ |
| **GrabCookie** | **GrabCssPropertyFrom** | **GrabCssPropertyFromAll** | **GrabCurrentUrl** |
| ~~GrabCurrentWindowHandle~~ | **GrabElementBoundingRect** | ~~GrabGeoLocation~~ | **GrabHTMLFrom** |
| GrabHTMLFromAll| ~~grabNumberOfOpenTabs~~ | grabNumberOfVisibleElements | GrabPageScrollPosition |
| ~~GrabPopupText~~ | GrabSource | **GrabTextFrom** | **GrabTextFromAll** |
| **GrabTitle** | **GrabValueFrom** | **GrabValueFromAll** |  |

#### Element Grabs

|   |   |   |   |
|---|---|---|---|
| **GrabAttribute** | **GrabCssProperty** | **GrabBoundingRect** | **GrabHTML** |
| **GrabText** | **GrabValue** |  |  |

## Waits

|   |   |   |   |
|---|---|---|---|
| **Wait** | **WaitForClickable** | WaitForDetached | **WaitForElement** |
| **WaitForEnabled** | WaitForFunction | **WaitForInvisible** | WaitForNavigation |
| WaitForRequest | WaitForResponse | WaitForText | WaitForValue |
| **WaitForVisible** | WaitInUrl | WaitNumberOfVisibleElements | WaitToHide |
| WaitUntil | WaitUrlEquals |

## Other


|   |   |   |   |
|---|---|---|---|
| AcceptPopup | DefineTimeout |