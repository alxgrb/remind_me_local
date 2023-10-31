#############################
# reminder app ver.2.0
# installation & description
# source link for this install script https://automationadmin.com/2016/05/how-to-run-ps-as-a-scheduled-task/ 
#############################
Clear-Host
$instdir = "$env:LOCALAPPDATA\.bin"
$uName = "$env:username"
$tName = "remindme"
New-Item $instdir -Type Directory -Force #-WhatIf
$sourcedir = Get-ChildItem -Path $PSScriptRoot
Copy-Item $sourcedir -Destination $instdir -Force #-WhatIf
$tCommand = "$instdir\remstart.cmd"
$tAction = New-ScheduledTaskAction -Execute "$tCommand" -WorkingDirectory $instdir
$tTrigger = New-ScheduledTaskTrigger -Daily -At 12:00
Register-ScheduledTask -Action $tAction -Trigger $tTrigger -TaskName "$tName" -User $uName -Description "The Reminder"