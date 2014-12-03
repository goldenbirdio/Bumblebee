﻿using Bumblebee.Interfaces;
using Bumblebee.KendoUI;
using Bumblebee.Setup;

using OpenQA.Selenium;

namespace Bumblebee.IntegrationTests.Shared.Pages.KendoUI
{
    public class KendoDropDownListDemoPage : WebBlock
    {
        public KendoDropDownListDemoPage(Session session)
            : base(session)
        {
        }

        public ISelectBox<KendoDropDownListDemoPage> Colors
        {
            get { return new KendoDropDownList<KendoDropDownListDemoPage>(this, By.Id("color")); }
        }

        public ISelectBox Sizes
        {
            get { return new KendoDropDownList(this, By.Id("size")); }
        }
    }
}