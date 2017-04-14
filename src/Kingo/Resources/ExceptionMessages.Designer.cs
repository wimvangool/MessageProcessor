﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kingo.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Kingo.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type &apos;{0}&apos; cannot publish event of type &apos;{1}&apos; because it has been removed from its repository and its lifetime has therefore ended..
        /// </summary>
        internal static string AggregateRoot_AggregateRemovedException {
            get {
                return ResourceManager.GetString("AggregateRoot_AggregateRemovedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not convert event of type &apos;{0}&apos; to an instance of type &apos;{1}&apos;. Please review the {2}() method of this event to ensure it returns the correct event type..
        /// </summary>
        internal static string AggregateRoot_EventConversionException {
            get {
                return ResourceManager.GetString("AggregateRoot_EventConversionException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Another handler for event of type &apos;{0}&apos; has already been added to this collection..
        /// </summary>
        internal static string AggregateRoot_HandlerForEventTypeAlreadyAdded {
            get {
                return ResourceManager.GetString("AggregateRoot_HandlerForEventTypeAlreadyAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id &apos;{0}&apos; on event &apos;{1}&apos; does not match the identifier of the aggregate it is being applied to ({2})..
        /// </summary>
        internal static string AggregateRoot_InvalidIdOnEvent {
            get {
                return ResourceManager.GetString("AggregateRoot_InvalidIdOnEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing event handler for event of type &apos;{0}&apos;..
        /// </summary>
        internal static string AggregateRoot_MissingEventHandlerException {
            get {
                return ResourceManager.GetString("AggregateRoot_MissingEventHandlerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The next version ({0}) must represent a newer version than the current version ({1})..
        /// </summary>
        internal static string AggregateRoot_VersionUpdateException {
            get {
                return ResourceManager.GetString("AggregateRoot_VersionUpdateException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified object of type &apos;{0}&apos; cannot be compared to instance of type &apos;{1}&apos;..
        /// </summary>
        internal static string Comparable_IncomparableType {
            get {
                return ResourceManager.GetString("Comparable_IncomparableType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot start this scope because a more restrive scope is still active..
        /// </summary>
        internal static string Context_IllegalScopeStarted {
            get {
                return ResourceManager.GetString("Context_IllegalScopeStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command &apos;{0}&apos; failed..
        /// </summary>
        internal static string DomainModelException_CommandFailed {
            get {
                return ResourceManager.GetString("DomainModelException_CommandFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing event &apos;{0}&apos;..
        /// </summary>
        internal static string DomainModelException_EventFailed {
            get {
                return ResourceManager.GetString("DomainModelException_EventFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ErrorLevel must be 0 or higher: {0}..
        /// </summary>
        internal static string ErrorLevel_InvalidErrorLevel {
            get {
                return ResourceManager.GetString("ErrorLevel_InvalidErrorLevel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The query could not be executed because of a bad request. See inner exception for details..
        /// </summary>
        internal static string ExecuteAsyncMethod_BadRequest {
            get {
                return ResourceManager.GetString("ExecuteAsyncMethod_BadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while executing the query. See inner exception for details..
        /// </summary>
        internal static string ExecuteAsyncMethod_InternalServerError {
            get {
                return ResourceManager.GetString("ExecuteAsyncMethod_InternalServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified expression is not supported: &apos;{0}&apos;..
        /// </summary>
        internal static string ExpressionExtensions_UnsupportedExpression {
            get {
                return ResourceManager.GetString("ExpressionExtensions_UnsupportedExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing command of type &apos;{0}&apos;. See inner exception for details..
        /// </summary>
        internal static string HandleInputStreamAsyncMethod_CommandHandlerException {
            get {
                return ResourceManager.GetString("HandleInputStreamAsyncMethod_CommandHandlerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid {0} specified: &apos;{1}&apos;..
        /// </summary>
        internal static string HandleInputStreamAsyncMethod_InvalidUnitOfWorkScopeOption {
            get {
                return ResourceManager.GetString("HandleInputStreamAsyncMethod_InvalidUnitOfWorkScopeOption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing event of type &apos;{0}&apos;. See inner exception for details..
        /// </summary>
        internal static string HandleStreamAsyncMethod_EventHandlerException {
            get {
                return ResourceManager.GetString("HandleStreamAsyncMethod_EventHandlerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create an empty identifier..
        /// </summary>
        internal static string Identifier_EmptyIdentifier {
            get {
                return ResourceManager.GetString("Identifier_EmptyIdentifier", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid identifier specified: &apos;{0}&apos;..
        /// </summary>
        internal static string Identifier_InvalidIdentifier {
            get {
                return ResourceManager.GetString("Identifier_InvalidIdentifier", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing message of type &apos;{0}&apos;. See inner exception for details..
        /// </summary>
        internal static string InternalServerErrorException_FromException {
            get {
                return ResourceManager.GetString("InternalServerErrorException_FromException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Two or more attributes declared on message &apos;{0}&apos; are assignable to &apos;{1}&apos;..
        /// </summary>
        internal static string Message_AmbiguousAttributeMatch {
            get {
                return ResourceManager.GetString("Message_AmbiguousAttributeMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot publish messages to the output stream while handling a metadata message..
        /// </summary>
        internal static string MessageHandlerContext_NullOutputStream_PublishNotAllowed {
            get {
                return ResourceManager.GetString("MessageHandlerContext_NullOutputStream_PublishNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid InstanceLifetime specified: &apos;{0}&apos;..
        /// </summary>
        internal static string MessageHandlerFactory_InvalidInstanceLifetime {
            get {
                return ResourceManager.GetString("MessageHandlerFactory_InvalidInstanceLifetime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message of type &apos;{0}&apos; is not valid: {1} validation error(s) found..
        /// </summary>
        internal static string MessageValidationPipeline_InvalidMessage {
            get {
                return ResourceManager.GetString("MessageValidationPipeline_InvalidMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot obtain single attribute of type &apos;{0}&apos; declared on method &apos;{1}&apos; because multiple attributes are of type &apos;{0}&apos;..
        /// </summary>
        internal static string MethodAttributeProvider_AmbiguousAttributeMatch {
            get {
                return ResourceManager.GetString("MethodAttributeProvider_AmbiguousAttributeMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invocation of method &apos;{0}()&apos; is not expected at this point..
        /// </summary>
        internal static string MicroProcessorContextState_InvalidOperation {
            get {
                return ResourceManager.GetString("MicroProcessorContextState_InvalidOperation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cache can only be used while a MicroProcessor is handling a message or executing a query..
        /// </summary>
        internal static string NullCache_CacheNotSupported {
            get {
                return ResourceManager.GetString("NullCache_CacheNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operation &apos;{0}&apos; is not supported at this point..
        /// </summary>
        internal static string NullController_OperationNotSupported {
            get {
                return ResourceManager.GetString("NullController_OperationNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot publish event of type &apos;{0}&apos;: events can only be published while a MicroProcessor is handling a message or executing a query..
        /// </summary>
        internal static string NullStream_PublishNotSupported {
            get {
                return ResourceManager.GetString("NullStream_PublishNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot publish messages to the output stream while executing a query..
        /// </summary>
        internal static string QueryContext_NullOutputStream_PublishNotAllowed {
            get {
                return ResourceManager.GetString("QueryContext_NullOutputStream_PublishNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no element at index &apos;{0}&apos; (Count = {1})..
        /// </summary>
        internal static string ReadOnlyList_IndexOutOfRange {
            get {
                return ResourceManager.GetString("ReadOnlyList_IndexOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type &apos;{0}&apos; with Id &apos;{1}&apos; was not found in the data store..
        /// </summary>
        internal static string Repository_AggregateNotFound {
            get {
                return ResourceManager.GetString("Repository_AggregateNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aggregate of type &apos;{0}&apos; with Id &apos;{1}&apos; could not be retrieved because it was removed from the data store in this session..
        /// </summary>
        internal static string Repository_AggregateRemovedInSession {
            get {
                return ResourceManager.GetString("Repository_AggregateRemovedInSession", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while restoring aggregate of type &apos;{0}&apos;. See inner exception for details..
        /// </summary>
        internal static string Repository_AggregateRestoreException {
            get {
                return ResourceManager.GetString("Repository_AggregateRestoreException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot add aggregate of type &apos;{0}&apos; to the repository because another aggregate with Id &apos;{1}&apos; is already present in the data store..
        /// </summary>
        internal static string Repository_DuplicateKeyException_AggregateAlreadyExists {
            get {
                return ResourceManager.GetString("Repository_DuplicateKeyException_AggregateAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot complete this scope because it is not the current scope..
        /// </summary>
        internal static string Scope_CannotCompleteScope {
            get {
                return ResourceManager.GetString("Scope_CannotCompleteScope", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The scopes were not nested correctly..
        /// </summary>
        internal static string Scope_IncorrectNesting {
            get {
                return ResourceManager.GetString("Scope_IncorrectNesting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The scope has already completed..
        /// </summary>
        internal static string Scope_ScopeAlreadyCompleted {
            get {
                return ResourceManager.GetString("Scope_ScopeAlreadyCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot obtain single attribute of type &apos;{0}&apos; declared on type &apos;{1}&apos; because multiple attributes are of type &apos;{0}&apos;..
        /// </summary>
        internal static string TypeAttributeProvider_AmbiguousAttributeMatch {
            get {
                return ResourceManager.GetString("TypeAttributeProvider_AmbiguousAttributeMatch", resourceCulture);
            }
        }
    }
}
