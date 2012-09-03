param($installPath, $toolsPath, $package, $project)

$contentFolderItem = $project.ProjectItems.Item("Content")
$aboutFolderItem = $contentFolderItem.ProjectItems.Item("About")
$aboutStylesItem = $aboutFolderItem.ProjectItems.Item("AboutStyles.xaml")

$aboutStylesItem.Properties.Item("BuildAction").Value = [int]2