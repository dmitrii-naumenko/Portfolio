Ground control center software deployed at 20+ sites in 2 countries to control 30+ satellites in orbit. There are control centers in [Siberian State University
Science and Technology](https://sat.sibsau.ru/page/doca-n), [Bauman Moscow State Technical University](https://sm.bmstu.ru/faculty/mkc/134-centr-upravleniya-poletami-mgtu-cup-b.html) and Lomonosov Moscow State University

 It includes following components:

- Workstation application
	- satellite tracking
	- control of RF equipments and modems
	- receiving and transmitting the data
	- task scheduller 
- Data storage (MS SQL Server)
	- metadata storage
	- telemetry storage
	- orbit parameters storage and versification
- Processing components (.NET C#, WPF, VBA, MS Office)
	- error correction
	- planning
	- data analysis
	- preparing reports
- Data visializaton (.NET C#, WPF)
	- interactive GUI with customized workspaces
	- preparing reports
	- helper for planing

It is main windows of workstation application
![](https://github.com/dmitrii-naumenko/Portfolio/blob/main/dotNET/Ground%20Control%20Center%20for%20small%20satellites/GCC%20main%20screen.png?raw=true)
	