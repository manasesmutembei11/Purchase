﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Templates
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class TemplateResource
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public TemplateResource()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("XpaSP.Core.Domain.Templates.TemplateResource", typeof(TemplateResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///
        ///&lt;html lang=&quot;en&quot; xmlns=&quot;http://www.w3.org/1999/xhtml&quot;&gt;
        ///&lt;head&gt;
        ///    &lt;meta charset=&quot;utf-8&quot; /&gt;
        ///    &lt;title&gt;&lt;/title&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///
        ///    &lt;p&gt;Dear {firstName}&lt;/p&gt;
        ///    &lt;p&gt;Welcome&lt;/p&gt;
        ///    &lt;p&gt;Please click the link below to confirm your email. Upon confirming you will receive another email with a link to set your password&lt;/p&gt;
        ///    &lt;p&gt;
        ///        &lt;a href=&quot;{callback}&quot;&gt;Confirm Email&lt;/a&gt;
        ///    &lt;/p&gt;
        ///    
        ///
        ///
        ///
        ///&lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        public static string EmailConfirmation
        {
            get
            {
                return ResourceManager.GetString("EmailConfirmation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///
        ///&lt;html lang=&quot;en&quot; xmlns=&quot;http://www.w3.org/1999/xhtml&quot;&gt;
        ///&lt;head&gt;
        ///    &lt;meta charset=&quot;utf-8&quot; /&gt;
        ///    &lt;title&gt;&lt;/title&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///
        ///    &lt;p&gt;Dear {FirstName}&lt;/p&gt;
        ///    &lt;p&gt;Welcome back&lt;/p&gt;
        ///    &lt;p&gt;Please set you password by clicking on the link below&lt;/p&gt;
        ///
        ///
        ///    &lt;p&gt;
        ///        &lt;a href=&quot;{callback}&quot;&gt;{action}&lt;/a&gt;
        ///    &lt;/p&gt;
        ///   
        ///
        ///
        ///
        ///&lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        public static string ResetPassword
        {
            get
            {
                return ResourceManager.GetString("ResetPassword", resourceCulture);
            }
        }
    }
}
