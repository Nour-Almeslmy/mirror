﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace BusinessLogicLayer.checkDataProfileStatus {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MEAI_OnlineServices_2017_webServices_GO_checkDataProfileStatus_V2_WSDL_Binder", Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class MEAI_OnlineServices_2017webServicesGOcheckDataProfileStatus_V2_WSDL : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback checkDataProfileStatus_V2OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public MEAI_OnlineServices_2017webServicesGOcheckDataProfileStatus_V2_WSDL() {
            this.Url = global::BusinessLogicLayer.Properties.Settings.Default.BusinessLogicLayer_checkDataProfileStatus_MEAI_OnlineServices_2017_webServices_GO_checkDataProfileStatus_V2_WSDL;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event checkDataProfileStatus_V2CompletedEventHandler checkDataProfileStatus_V2Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("MEAI_OnlineServices_2017_webServices_GO_checkDataProfileStatus_V2_WSDL_Binder_che" +
            "ckDataProfileStatus_V2", RequestNamespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO", ResponseNamespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("checkDataProfileStatus_out", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public checkDataProfileStatus_out checkDataProfileStatus_V2([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)] checkDataProfileStatus_in checkDataProfileStatus_in) {
            object[] results = this.Invoke("checkDataProfileStatus_V2", new object[] {
                        checkDataProfileStatus_in});
            return ((checkDataProfileStatus_out)(results[0]));
        }
        
        /// <remarks/>
        public void checkDataProfileStatus_V2Async(checkDataProfileStatus_in checkDataProfileStatus_in) {
            this.checkDataProfileStatus_V2Async(checkDataProfileStatus_in, null);
        }
        
        /// <remarks/>
        public void checkDataProfileStatus_V2Async(checkDataProfileStatus_in checkDataProfileStatus_in, object userState) {
            if ((this.checkDataProfileStatus_V2OperationCompleted == null)) {
                this.checkDataProfileStatus_V2OperationCompleted = new System.Threading.SendOrPostCallback(this.OncheckDataProfileStatus_V2OperationCompleted);
            }
            this.InvokeAsync("checkDataProfileStatus_V2", new object[] {
                        checkDataProfileStatus_in}, this.checkDataProfileStatus_V2OperationCompleted, userState);
        }
        
        private void OncheckDataProfileStatus_V2OperationCompleted(object arg) {
            if ((this.checkDataProfileStatus_V2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.checkDataProfileStatus_V2Completed(this, new checkDataProfileStatus_V2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class checkDataProfileStatus_in {
        
        private string dialField;
        
        private string sourceIdField;
        
        private string langIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string dial {
            get {
                return this.dialField;
            }
            set {
                this.dialField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string sourceId {
            get {
                return this.sourceIdField;
            }
            set {
                this.sourceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string langId {
            get {
                return this.langIdField;
            }
            set {
                this.langIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class errorDoc {
        
        private string statusField;
        
        private string errorCodeField;
        
        private string errorMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string errorCode {
            get {
                return this.errorCodeField;
            }
            set {
                this.errorCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string errorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class optionsList {
        
        private string optionNameField;
        
        private string optionIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string optionName {
            get {
                return this.optionNameField;
            }
            set {
                this.optionNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string optionId {
            get {
                return this.optionIdField;
            }
            set {
                this.optionIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class BucketInfo {
        
        private string currentBucketIdField;
        
        private string currentBucketNameField;
        
        private string isRnRField;
        
        private string rnRTextField;
        
        private optionsList[] optionsListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string currentBucketId {
            get {
                return this.currentBucketIdField;
            }
            set {
                this.currentBucketIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string currentBucketName {
            get {
                return this.currentBucketNameField;
            }
            set {
                this.currentBucketNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string IsRnR {
            get {
                return this.isRnRField;
            }
            set {
                this.isRnRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string RnRText {
            get {
                return this.rnRTextField;
            }
            set {
                this.rnRTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("optionsList", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public optionsList[] optionsList {
            get {
                return this.optionsListField;
            }
            set {
                this.optionsListField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://eaidev.mobinil.com/MEAI_OnlineServices_2017.webServices.GO")]
    public partial class checkDataProfileStatus_out {
        
        private BucketInfo[] bucketInfoField;
        
        private errorDoc errorDocField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BucketInfo", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public BucketInfo[] BucketInfo {
            get {
                return this.bucketInfoField;
            }
            set {
                this.bucketInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public errorDoc errorDoc {
            get {
                return this.errorDocField;
            }
            set {
                this.errorDocField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void checkDataProfileStatus_V2CompletedEventHandler(object sender, checkDataProfileStatus_V2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class checkDataProfileStatus_V2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal checkDataProfileStatus_V2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public checkDataProfileStatus_out Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((checkDataProfileStatus_out)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591