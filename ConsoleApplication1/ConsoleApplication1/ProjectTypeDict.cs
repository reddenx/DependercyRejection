using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependercyRejectionUI
{
    public class ProjectTypeDict : Dictionary<Guid, string>
    {
        public ProjectTypeDict()
        {
        }

        public static Dictionary<Guid, string> Factory()
        {
            return new Dictionary<Guid, string>()
            {
                {new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"),"Windows (C#)"},
                {new Guid("F184B08F-C81C-45F6-A57F-5ABD9991F28F"),"Windows (VB.NET)"},
                {new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942"),"Windows (Visual C++)"},
                {new Guid("349C5851-65DF-11DA-9384-00065B846F21"),"Web Application"},
                {new Guid("E24C65DC-7377-472B-9ABA-BC803B73C61A"),"Web Site"},
                {new Guid("F135691A-BF7E-435D-8960-F99683D2D49C"),"Distributed System"},
                {new Guid("3D9AD99F-2412-4246-B90B-4EAA41C64699"),"Windows Communication Foundation (WCF)"},
                {new Guid("60DC8134-EBA5-43B8-BCC9-BB4BC16C2548"),"Windows Presentation Foundation (WPF)"},
                {new Guid("C252FEB5-A946-4202-B1D4-9916A0590387"),"Visual Database Tools"},
                {new Guid("A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124"),"Database"},
                {new Guid("4F174C21-8C12-11D0-8340-0000F80270F8"),"Database (other project types)"},
                {new Guid("3AC096D0-A1C2-E12C-1390-A8335801FDAB"),"Test"},
                {new Guid("66A26720-8FB5-11D2-AA7E-00C04F688DDE"),"Solution Folder"},
                {new Guid("14822709-B5A1-4724-98CA-57A101D1B079"),"Workflow (C#)"},
                {new Guid("D59BE175-2ED0-4C54-BE3D-CDAA9F3214C8"),"Workflow (VB.NET)"},
                {new Guid("06A35CCD-C46D-44D5-987B-CF40FF872267"),"Deployment Merge Module"},
                {new Guid("3EA9E505-35AC-4774-B492-AD1749C4943A"),"Deployment Cab"},
                {new Guid("978C614F-708E-4E1A-B201-565925725DBA"),"Deployment Setup"},
                {new Guid("AB322303-2255-48EF-A496-5904EB18DA55"),"Deployment Smart Device Cab"},
                {new Guid("E6FDF86B-F3D1-11D4-8576-0002A516ECE8"),"Visual J#"},
                {new Guid("F8810EC1-6754-47FC-A15F-DFABD2E3FA90"),"SharePoint Workflow"},
                {new Guid("593B0543-81F6-4436-BA1E-4747859CAAE2"), "SharePoint (C#)"}
            };
        }
    }
}
