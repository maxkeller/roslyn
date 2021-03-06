' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.ComponentModel.Composition
Imports Microsoft.CodeAnalysis.Editor
Imports Microsoft.VisualStudio.Editor
Imports Microsoft.VisualStudio.LanguageServices.Interactive
Imports Microsoft.VisualStudio.OLE.Interop
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Text.Editor
Imports Microsoft.VisualStudio.Utilities
Imports Roslyn.Editor.InteractiveWindow
Imports Roslyn.Editor.InteractiveWindow.Commands
Imports Roslyn.VisualStudio.InteractiveWindow

Namespace Microsoft.VisualStudio.LanguageServices.VisualBasic.Interactive

    <Export(GetType(IVsInteractiveWindowOleCommandTargetProvider))>
    <ContentType(ContentTypeNames.VisualBasicContentType)>
    <ContentType(PredefinedInteractiveCommandsContentTypes.InteractiveCommandContentTypeName)>
    Friend NotInheritable Class VisualBasicVsInteractiveWindowCommandProvider
        Implements IVsInteractiveWindowOleCommandTargetProvider

        Private ReadOnly editorAdaptersFactory As IVsEditorAdaptersFactoryService
        Private ReadOnly commandHandlerServiceFactory As ICommandHandlerServiceFactory
        Private ReadOnly serviceProvider As System.IServiceProvider

        <ImportingConstructor()>
        Public Sub New(commandHandlerServiceFactory As ICommandHandlerServiceFactory, editorAdaptersFactoryService As IVsEditorAdaptersFactoryService, serviceProvider As SVsServiceProvider)
            Me.commandHandlerServiceFactory = commandHandlerServiceFactory
            Me.editorAdaptersFactory = editorAdaptersFactoryService
            Me.serviceProvider = serviceProvider
        End Sub

        Public Function GetCommandTarget(textView As IWpfTextView, nextTarget As IOleCommandTarget) As IOleCommandTarget _
            Implements IVsInteractiveWindowOleCommandTargetProvider.GetCommandTarget

            Dim target = New ScriptingOleCommandTarget(textView, commandHandlerServiceFactory, editorAdaptersFactory, serviceProvider)
            target.RefreshCommandFilters()
            target.NextCommandTarget = nextTarget
            Return target
        End Function
    End Class
End Namespace

