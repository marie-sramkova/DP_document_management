   M S B A M L   `   `   `  ���� P  LDocumentManagementApp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:DocumentManagementAppDocumentManagementApp  #    DocumentManagementApp.Window1   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:DocumentManagementApp     -     � Title$  Analyze dokument�� � WindowState$ 	Maximized=�.����  �� Padding$ 5q�._��  � ColumnDefinitions �� $��* ��� $��10 ��� $��* �.+�]� $��0��.+�]� $
��Left=�$	��Top=�$��30��.+��� -    ��Name$ 
buttonBack��$��0��$��50��$
��Left=�.���� 	<- Back]� $��Bottom=�$��30��.+��� -   $ 
buttonSave��$
��Save��]� $��Center=�$��False��.+��  � RowDefinitions �� -    ��Name$ listView��$	��91* ��� -   $ 	listView2��$��0* ��� -   $ panel��$��0* �.+�� $��Center=�$��Center=�$��0��$��20q�.+��� -    ��Name$ listViewTemplates��
������ $��0q�$��False������ *BindingTemplates+���� .R�� .+��� ���� *Name+�� ���� *SimilarityPercentage+� $��Stretch=�$��1��$��20q�.+��� -   $��listViewAttributes��
������ $��0q�$��False������ *BindingAttributes+���� .R�� .+��� $	��100��$
��Left=�$��center=����� *Name+��  ��Name$ txtBox��-	   $	��300��$��Stretch=�$	 �� HorizontalScrollBarVisibility$	 Visible=�"
 �� VerticalScrollBarVisibility$
 Visible=����� *Value+ �� UpdateSourceTrigger$ PropertyChanged=� �� Delay$
 2000���� $��10, 0, 10, 0q�$��70��$��center=����� *Type+�� -
   $��Delete��$
��Bold"�$��30��$��70��$��Right=�� $��2��$��10q�.+��� -    ��Name$$ templateAndAttributeStackPanel��$��Visible=�$��False���� $��1�� �� ResizeDirection$ Columns=� �� ResizeBehavior$ PreviousAndNext=�$��Stretch=����#FFFFFF���� .R�� .+��� $	��⁞���� ���	#00FFFFFF]� $��2��.+�]� $	��Top=�$��30��$��Stretch=�$
��True��.+��� -   $ 
buttonLeft��$��50��$
��Left=�.���� <<�� -   $ buttonRight��$��50��$��Right=�$��>>���� -   $��labelSelectedFile��$��filename.docx��]� $��Bottom=�.+��� -    ��Name$ imgAnalyzedDocument��