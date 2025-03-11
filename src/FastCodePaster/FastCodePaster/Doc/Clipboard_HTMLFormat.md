**StartHTML** 

The byte offset where the HTML content begins.

**EndHTML** 

The byte offset where the HTML content ends.
**StartFragment** 

 The byte offset where the main content fragment begins.

**EndFragment** 

The byte offset where the main content fragment ends.



### How These Offsets Are Calculated
#### StartHTML & EndHTML

**StartHTML** is the byte position (zero-based) of the first <HTML> tag in the clipboard data.

**EndHTML** is the position right after the last character of the </HTML> tag.
These positions define the full range of the HTML document.

#### StartFragment & EndFragment

**StartFragment** is the position of the <div id="StartFragment"> tag.
**EndFragment** is the position of the closing </div> tag that marks the end of the fragment.
These positions define the user-selected (or relevant) portion of the HTML content.
Example: HTML Clipboard Format

```
Version:0.9
StartHTML:00000097
EndHTML:00000250
StartFragment:00000130
EndFragment:00000200
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<HEAD>
<TITLE>Clipboard Example</TITLE>
</HEAD>
<BODY>
<!--StartFragment--><p>Hello, <b>world</b>!</p><!--EndFragment-->
</BODY>
</HTML>
```



**StartHTML (97)** 

The <HTML> tag starts at byte 97.

**EndHTML (250)** 

The </HTML> tag ends at byte 250.

**StartFragment (130)** 

The start of the fragment (<!--StartFragment-->) is at byte 130.



**EndFragment (200)** 

The end of the fragment (<!--EndFragment-->) is at byte 200.



### How to Manually Compute These Offsets
Write the clipboard content as a string.
Count characters (including spaces and newlines) from the beginning of the string.
Determine the exact byte offsets where <HTML>, <!--StartFragment-->, <!--EndFragment-->, and </HTML> appear.



### Reference Document

https://learn.microsoft.com/en-us/windows/win32/dataxchg/html-clipboard-format