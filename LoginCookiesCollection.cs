using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuterTube
{
    /// <summary>
    /// The collection of cookies used by Youtube and YT Music when an account is logged in'
    /// </summary>
    public class LoginData
    {
        private const string DOMAIN = ".youtube.com";
        private const string PATH = "/";
        private const string MEDIUM_PRIORITY = "medium";
        private const string HIGH_PRIORITY = "high";
        public required (string, string) APISID
        {
            set => (APISIDCookie.Value, APISIDCookie.Expires) = value;
        }

        public required (string, string) CONSISTENCY
        {
            set => (CONSISTENCYCookie.Value, CONSISTENCYCookie.Expires) = value;
        }

        public required (string, string) GPS
        {
            set => (GPSCookie.Value, GPSCookie.Expires) = value;
        }

        public required (string, string) HSID
        {
            set => (HSIDCookie.Value, HSIDCookie.Expires) = value;
        }

        public required (string, string) LOGIN_INFO
        {
            set => (LOGIN_INFOCookie.Value, LOGIN_INFOCookie.Expires) = value;
        }

        public required (string, string) PREF
        {
            set => (PREFCookie.Value, PREFCookie.Expires) = value;
        }

        public required (string, string) SAPISID
        {
            set => (SAPISIDCookie.Value, SAPISIDCookie.Expires) = value;
        }

        public required (string, string) SID
        {
            set => (SIDCookie.Value, SIDCookie.Expires) = value;
        }

        public required (string, string) SIDCC
        {
            set => (SIDCCCookie.Value, SIDCCCookie.Expires) = value;
        }

        public required (string, string) SSID
        {
            set => (SSIDCookie.Value, SSIDCookie.Expires) = value;
        }

        public required (string, string) ST_xuwub9
        {
            set => (ST_xuwub9Cookie.Value, ST_xuwub9Cookie.Expires) = value;
        }

        public required (string, string) VISITOR_INFO1_LIVE
        {
            set => (VISITOR_INFO1_LIVECookie.Value, VISITOR_INFO1_LIVECookie.Expires) = value;
        }

        public required (string, string) VISITOR_PRIVACY_METADATA
        {
            set => (VISITOR_PRIVACY_METADATACookie.Value, VISITOR_PRIVACY_METADATACookie.Expires) = value;
        }

        public required (string, string) YSC
        {
            set => (YSCCookie.Value, YSCCookie.Expires) = value;
        }

        public required (string, string) __Secure_1PAPISID
        {
            set => (__Secure_1PAPISIDCookie.Value, __Secure_1PAPISIDCookie.Expires) = value;
        }

        public required (string, string) __Secure_1PSID
        {
            set => (__Secure_1PSIDCookie.Value, __Secure_1PSIDCookie.Expires) = value;
        }

        public required (string, string) __Secure_1PSIDCC
        {
            set => (__Secure_1PSIDCCCookie.Value, __Secure_1PSIDCCCookie.Expires) = value;
        }

        public required (string, string) __Secure_1PSIDTS
        {
            set => (__Secure_1PSIDTSCookie.Value, __Secure_1PSIDTSCookie.Expires) = value;
        }

        public required (string, string) __Secure_3PAPISID
        {
            set => (__Secure_3PAPISIDCookie.Value, __Secure_3PAPISIDCookie.Expires) = value;
        }

        public required (string, string) __Secure_3PSID
        {
            set => (__Secure_3PSIDCookie.Value, __Secure_3PSIDCookie.Expires) = value;
        }

        public required (string, string) __Secure_3PSIDCC
        {
            set => (__Secure_3PSIDCCCookie.Value, __Secure_3PSIDCCCookie.Expires) = value;
        }

        public required (string, string) __Secure_3PSIDTS
        {
            set => (__Secure_3PSIDTSCookie.Value, __Secure_3PSIDTSCookie.Expires) = value;
        }



        private Cookie APISIDCookie { get; set; } = new(DOMAIN, PATH, "APISID", null, null, false, false, null, HIGH_PRIORITY);
        private Cookie CONSISTENCYCookie { get; set; } = new(DOMAIN, PATH, "CONSISTENCY", null, null, false, true, null, MEDIUM_PRIORITY);
        private Cookie GPSCookie { get; set; } = new(DOMAIN, PATH, "GPS", null, null, true, true, null, MEDIUM_PRIORITY);
        private Cookie HSIDCookie { get; set; } = new(DOMAIN, PATH, "HSID", null, null, true, false, null, HIGH_PRIORITY);
        private Cookie LOGIN_INFOCookie { get; set; } = new(DOMAIN, PATH, "LOGIN_INFO", null, null, true, true, "none", MEDIUM_PRIORITY);
        private Cookie PREFCookie { get; set; } = new(DOMAIN, PATH, "PREF", null, null, false, true, null, MEDIUM_PRIORITY);
        private Cookie SAPISIDCookie { get; set; } = new(DOMAIN, PATH, "SAPISID", null, null, false, true, null, HIGH_PRIORITY);
        private Cookie SIDCookie { get; set; } = new(DOMAIN, PATH, "SID", null, null, false, false, null, HIGH_PRIORITY);
        private Cookie SIDCCCookie { get; set; } = new(DOMAIN, PATH, "SID-CC", null, null, false, false, null, HIGH_PRIORITY);
        private Cookie SSIDCookie { get; set; } = new(DOMAIN, PATH, "SSID", null, null, true, true, null, HIGH_PRIORITY);
        private Cookie ST_xuwub9Cookie { get; set; } = new(DOMAIN, PATH, "ST-xuwub9", null, null, false, false, null, MEDIUM_PRIORITY);
        private Cookie VISITOR_INFO1_LIVECookie { get; set; } = new(DOMAIN, PATH, "VISITOR_INFO1_LIVE", null, null, false, false, "none", MEDIUM_PRIORITY);
        private Cookie VISITOR_PRIVACY_METADATACookie { get; set; } = new(DOMAIN, PATH, "VISITOR_PRIVACY_METADATA", null, null, false, false, "none", MEDIUM_PRIORITY);
        private Cookie YSCCookie { get; set; } = new(DOMAIN, PATH, "YSC", null, null, true, true, "none", HIGH_PRIORITY);
        private Cookie __Secure_1PAPISIDCookie { get; set; } = new(DOMAIN, PATH, "__Secure-1PAPISID", null, null, false, true, null, HIGH_PRIORITY);
        private Cookie __Secure_1PSIDCookie { get; set; } = new(DOMAIN, PATH, "__Secure-1PSID", null, null, true, true, null, HIGH_PRIORITY);
        private Cookie __Secure_1PSIDCCCookie { get; set; } = new(DOMAIN, PATH, "__Secure-1PSIDCC", null, null, true, true, null, HIGH_PRIORITY);
        private Cookie __Secure_1PSIDTSCookie { get; set; } = new(DOMAIN, PATH, "__Secure-1PSIDTS", null, null, true, true, "none", HIGH_PRIORITY);
        private Cookie __Secure_3PAPISIDCookie { get; set; } = new(DOMAIN, PATH, "__Secure-3PAPISID", null, null, false, true, "none", HIGH_PRIORITY);
        private Cookie __Secure_3PSIDCookie { get; set; } = new(DOMAIN, PATH, "__Secure-3PSID", null, null, true, true, null, HIGH_PRIORITY);
        private Cookie __Secure_3PSIDCCCookie { get; set; } = new(DOMAIN, PATH, "__Secure-3PSIDCC", null, null, true, true, "none", HIGH_PRIORITY);
        private Cookie __Secure_3PSIDTSCookie { get; set; } = new(DOMAIN, PATH, "__Secure-3PSIDTS", null, null, true, true, "none", HIGH_PRIORITY);

        public List<Cookie> GetCookiesList() => [APISIDCookie,
                                                CONSISTENCYCookie,
                                                GPSCookie,
                                                HSIDCookie,
                                                LOGIN_INFOCookie,
                                                PREFCookie,
                                                SAPISIDCookie,
                                                SIDCookie,
                                                SIDCCCookie,
                                                SSIDCookie,
                                                ST_xuwub9Cookie,
                                                VISITOR_INFO1_LIVECookie,
                                                VISITOR_PRIVACY_METADATACookie,
                                                YSCCookie,
                                                __Secure_1PAPISIDCookie,
                                                __Secure_1PSIDCookie,
                                                __Secure_1PSIDCCCookie,
                                                __Secure_1PSIDTSCookie,
                                                __Secure_3PAPISIDCookie,
                                                __Secure_3PSIDCookie,
                                                __Secure_3PSIDCCCookie,
                                                __Secure_3PSIDTSCookie];
    }

    /// <summary>
    /// The collection of cookies used by Youtube and YT Music when no account is logged in and the user already used the website
    /// </summary>
    public class VisitorData
    {
        private const string DOMAIN = ".youtube.com";
        private const string PATH = "/";
        private const string MEDIUM_PRIORITY = "medium";

        public required (string, string) CONSISTENCY
        {
            set => (CONSISTENCYCookie.Value, CONSISTENCYCookie.Expires) = value;
        }

        public required (string, string) GPS
        {
            set => (GPSCookie.Value, GPSCookie.Expires) = value;
        }

        public required (string, string) PREF
        {
            set => (PREFCookie.Value, PREFCookie.Expires) = value;
        }

        public required (string, string) VISITOR_INFO1_LIVE
        {
            set => (VISITOR_INFO1_LIVECookie.Value, VISITOR_INFO1_LIVECookie.Expires) = value;
        }

        public required (string, string) VISITOR_PRIVACY_METADATA
        {
            set => (VISITOR_PRIVACY_METADATACookie.Value, VISITOR_PRIVACY_METADATACookie.Expires) = value;
        }

        public required (string, string) YSC
        {
            set => (YSCCookie.Value, YSCCookie.Expires) = value;
        }

        private Cookie PREFCookie { get; set; } = new(DOMAIN, PATH, "PREF", null, null, false, true, null, MEDIUM_PRIORITY);
        private Cookie CONSISTENCYCookie { get; set; } = new(DOMAIN, PATH, "CONSISTENCY", null, null, false, true, null, MEDIUM_PRIORITY);
        private Cookie GPSCookie { get; set; } = new(DOMAIN, PATH, "GPS", null, null, true, true, null, MEDIUM_PRIORITY);
        private Cookie VISITOR_INFO1_LIVECookie { get; set; } = new(DOMAIN, PATH, "VISITOR_INFO1_LIVE", null, null, false, false, "none", MEDIUM_PRIORITY);
        private Cookie VISITOR_PRIVACY_METADATACookie { get; set; } = new(DOMAIN, PATH, "VISITOR_PRIVACY_METADATA", null, null, false, false, "none", MEDIUM_PRIORITY);
        private Cookie YSCCookie { get; set; } = new(DOMAIN, PATH, "YSC", null, null, true, true, "none", MEDIUM_PRIORITY);

        public List<Cookie> GetCookiesList() => [PREFCookie, CONSISTENCYCookie, GPSCookie, VISITOR_INFO1_LIVECookie, VISITOR_PRIVACY_METADATACookie, YSCCookie];
    }

    public class Cookie(string domain, string path, string name, string? value, string? expires, bool httpOnly, bool secure, string? sameSite, string priority)
    {
        public string Name { get; set; } = name;
        public string? Value { get; set; } = value;
        public string Domain { get; set; } = domain;
        public string Path { get; set; } = path;
        public bool HttpOnly { get; set; } = httpOnly;
        public bool Secure { get; set; } = secure;
        public string Priority { get; set; } = priority;
        public string? Expires { get; set; } = expires;
        public string? SameSite { get; set; } = sameSite;

        public override string ToString()
        {
            string cookieString = $"{Name}={Value}; expires={Expires}; path={Path}; priority={Priority}; domain={Domain};";
            if (Secure) cookieString += " Secure;";
            if (HttpOnly) cookieString += " HttpOnly;";
            if (SameSite is not null) cookieString += $" SameSite={SameSite};";

            return cookieString;
        }

    }
}
