﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWebClient.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SearchParameter", Namespace="http://schemas.datacontract.org/2004/07/NationalCriminalDatabase")]
    [System.SerializableAttribute()]
    public partial class SearchParameter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> AgeBeforeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> AgeFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FreeTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> HeightBeforeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> HeightFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] NationalityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SexField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> WeightBeforeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> WeightFromField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> AgeBefore {
            get {
                return this.AgeBeforeField;
            }
            set {
                if ((this.AgeBeforeField.Equals(value) != true)) {
                    this.AgeBeforeField = value;
                    this.RaisePropertyChanged("AgeBefore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> AgeFrom {
            get {
                return this.AgeFromField;
            }
            set {
                if ((this.AgeFromField.Equals(value) != true)) {
                    this.AgeFromField = value;
                    this.RaisePropertyChanged("AgeFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FreeText {
            get {
                return this.FreeTextField;
            }
            set {
                if ((object.ReferenceEquals(this.FreeTextField, value) != true)) {
                    this.FreeTextField = value;
                    this.RaisePropertyChanged("FreeText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> HeightBefore {
            get {
                return this.HeightBeforeField;
            }
            set {
                if ((this.HeightBeforeField.Equals(value) != true)) {
                    this.HeightBeforeField = value;
                    this.RaisePropertyChanged("HeightBefore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> HeightFrom {
            get {
                return this.HeightFromField;
            }
            set {
                if ((this.HeightFromField.Equals(value) != true)) {
                    this.HeightFromField = value;
                    this.RaisePropertyChanged("HeightFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Nationality {
            get {
                return this.NationalityField;
            }
            set {
                if ((object.ReferenceEquals(this.NationalityField, value) != true)) {
                    this.NationalityField = value;
                    this.RaisePropertyChanged("Nationality");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sex {
            get {
                return this.SexField;
            }
            set {
                if ((object.ReferenceEquals(this.SexField, value) != true)) {
                    this.SexField = value;
                    this.RaisePropertyChanged("Sex");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> WeightBefore {
            get {
                return this.WeightBeforeField;
            }
            set {
                if ((this.WeightBeforeField.Equals(value) != true)) {
                    this.WeightBeforeField = value;
                    this.RaisePropertyChanged("WeightBefore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> WeightFrom {
            get {
                return this.WeightFromField;
            }
            set {
                if ((this.WeightFromField.Equals(value) != true)) {
                    this.WeightFromField = value;
                    this.RaisePropertyChanged("WeightFrom");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.INcSearchService")]
    public interface INcSearchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INcSearchService/Search", ReplyAction="http://tempuri.org/INcSearchService/SearchResponse")]
        bool Search(TestWebClient.ServiceReference1.SearchParameter request, string email, int maxResultCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INcSearchService/Search", ReplyAction="http://tempuri.org/INcSearchService/SearchResponse")]
        System.Threading.Tasks.Task<bool> SearchAsync(TestWebClient.ServiceReference1.SearchParameter request, string email, int maxResultCount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INcSearchServiceChannel : TestWebClient.ServiceReference1.INcSearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NcSearchServiceClient : System.ServiceModel.ClientBase<TestWebClient.ServiceReference1.INcSearchService>, TestWebClient.ServiceReference1.INcSearchService {
        
        public NcSearchServiceClient() {
        }
        
        public NcSearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NcSearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NcSearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NcSearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Search(TestWebClient.ServiceReference1.SearchParameter request, string email, int maxResultCount) {
            return base.Channel.Search(request, email, maxResultCount);
        }
        
        public System.Threading.Tasks.Task<bool> SearchAsync(TestWebClient.ServiceReference1.SearchParameter request, string email, int maxResultCount) {
            return base.Channel.SearchAsync(request, email, maxResultCount);
        }
    }
}
