using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCodePaster
{
    public class FragementTemplate
    {
        public static string GetFragementTemplate(int index)
        {
            switch(index)
            {
                case 0:
                    //from Microsoft Learn
                    return "<span style=\"box-sizing: inherit; outline-color: inherit; margin: 1rem 0px 0px; padding: 0px; overflow-wrap: break-word; color: rgb(22, 22, 22); font-family: &quot;Segoe UI&quot;, SegoeUI, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;\"><code style=\"box-sizing: inherit; outline-color: inherit; font-family: SFMono-Regular, Consolas, &quot;Liberation Mono&quot;, Menlo, Courier, monospace; font-size: 13.6px; direction: ltr; background-color: var(--theme-inline-code); overflow-wrap: break-word; border-radius: 3px; padding: 0.1em 0.2em;\">{0}</code></span>";
                case 1:
                    return "<span style=\"color: rgb(192, 52, 29); font-family: ui-monospace, SFMono-Regular, &quot;SF Mono&quot;, Menlo, Consolas, &quot;Liberation Mono&quot;, monospace, sans-serif; font-size: 12px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(251, 229, 225); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;\">{0}</span>";
                default:
                    return "<p style=\"box-sizing: inherit; outline-color: inherit; margin: 1rem 0px 0px; padding: 0px; overflow-wrap: break-word; color: rgb(22, 22, 22); font-family: &quot;Segoe UI&quot;, SegoeUI, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;\"><code style=\"box-sizing: inherit; outline-color: inherit; font-family: SFMono-Regular, Consolas, &quot;Liberation Mono&quot;, Menlo, Courier, monospace; font-size: 13.6px; direction: ltr; background-color: var(--theme-inline-code); overflow-wrap: break-word; border-radius: 3px; padding: 0.1em 0.2em;\">{0}</code></p>";
            }
        }
    }
}
