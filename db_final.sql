USE [HouseHelper]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[Adid] [nvarchar](255) NOT NULL,
	[Adname] [nvarchar](255) NULL,
	[Addob] [date] NULL,
	[Adphone] [int] NULL,
	[Adgener] [bit] NULL,
	[Adaddress] [nvarchar](255) NULL,
	[Ademail] [nvarchar](255) NULL,
	[Adimg] [nvarchar](255) NULL,
	[Jobid] [nvarchar](255) NULL,
	[JobSeekerid] [nvarchar](255) NULL,
	[FindHelperid] [nvarchar](255) NULL,
	[CVid] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Adid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CV]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CV](
	[CVid] [nvarchar](50) NOT NULL,
	[Worktype] [nvarchar](50) NULL,
	[Workstartdate] [date] NULL,
	[Workactualstart] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[CVid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[findhelper]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[findhelper](
	[FindHelperid] [nvarchar](255) NOT NULL,
	[FindHelperName] [nvarchar](255) NULL,
	[FindHelperEmail] [nvarchar](255) NULL,
	[FindHelperImg] [nvarchar](255) NULL,
	[FindHelperPhone] [nvarchar](255) NULL,
	[FindHelperAddress] [nvarchar](255) NULL,
	[FindHelperDescription] [nvarchar](255) NULL,
	[FindHelperLocation] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[FindHelperid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[job]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[job](
	[Jobid] [nvarchar](255) NOT NULL,
	[JobName] [nvarchar](255) NULL,
	[JobType] [nvarchar](255) NULL,
	[JobStart] [date] NULL,
	[JobStartFlexibility] [nvarchar](255) NULL,
	[JobDatePost] [date] NULL,
	[JobGender] [nvarchar](255) NULL,
	[JobEducation] [nvarchar](255) NULL,
	[JobAge] [nvarchar](255) NULL,
	[JobExp] [nvarchar](255) NULL,
	[JobSalaryMin] [nvarchar](255) NULL,
	[JobSalryMax] [nvarchar](255) NULL,
	[JobImage] [nvarchar](255) NULL,
	[JobTitle] [nvarchar](255) NULL,
	[JobDescription] [nvarchar](1000) NULL,
	[FindHelperid] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Jobid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobseeker]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobseeker](
	[JobSeekerid] [nvarchar](255) NOT NULL,
	[JobSeekername] [nvarchar](255) NULL,
	[JobSeekerdob] [date] NULL,
	[JobSeekerphone] [nvarchar](255) NULL,
	[JobSeekergender] [bit] NULL,
	[JobSeekeraddress] [nvarchar](255) NULL,
	[JobSeekerimg] [nvarchar](255) NULL,
	[JobSeekerSalaryFrom] [nvarchar](255) NULL,
	[JobSeekerSalaryTo] [nvarchar](255) NULL,
	[JobSeekerDescription] [nvarchar](1000) NULL,
	[JobSeekerEmail] [nvarchar](50) NULL,
	[JobSeekerLocation] [nvarchar](50) NULL,
	[JobSeekerCVid] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobSeekerid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobseekercookingskill]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobseekercookingskill](
	[JobSeekerCookingSkillid] [nvarchar](255) NOT NULL,
	[JobSeekerCookingSkillname] [nvarchar](255) NULL,
	[JobSeekerid] [nvarchar](255) NULL,
	[Jobid] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobSeekerCookingSkillid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobseekerlanguage]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobseekerlanguage](
	[JobSeekerLanguageid] [nvarchar](255) NOT NULL,
	[JobSeekerLanguagename] [nvarchar](255) NULL,
	[JobSeekerid] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobSeekerLanguageid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobseekermainskill]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobseekermainskill](
	[JobSeekerMainSkillid] [nvarchar](255) NOT NULL,
	[JobSeekerMainSkillname] [nvarchar](255) NULL,
	[JobSeekerid] [nvarchar](255) NULL,
	[Jobid] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobSeekerMainSkillid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobseekerotherskill]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobseekerotherskill](
	[JobSeekerOtherSkillid] [nvarchar](255) NOT NULL,
	[JobSeekerOtherSkillname] [nvarchar](255) NULL,
	[JobSeekerid] [nvarchar](255) NULL,
	[Jobid] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JobSeekerOtherSkillid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 25/3/2023 5:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[UserID] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NULL,
	[Passwords] [nvarchar](255) NULL,
	[RoleId] [int] NULL,
	[Email] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[admin] ([Adid], [Adname], [Addob], [Adphone], [Adgener], [Adaddress], [Ademail], [Adimg], [Jobid], [JobSeekerid], [FindHelperid], [CVid]) VALUES (N'Admin', N'Admin', CAST(N'1998-11-13' AS Date), 867130599, 0, N'Việt Nam', N'Admin@gmail.com', N'327894529_697315605389405_198087486408950672_n.jpg', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[CV] ([CVid], [Worktype], [Workstartdate], [Workactualstart]) VALUES (N'Cv1ef05ca6-0ef4-499f-9108-9a770dc22952', N'Part Time', CAST(N'2020-01-04' AS Date), CAST(N'2023-03-25' AS Date))
INSERT [dbo].[CV] ([CVid], [Worktype], [Workstartdate], [Workactualstart]) VALUES (N'Cve0e166bc-719f-48a9-a18f-ee0cbbee6394', N'Full Time', CAST(N'2020-02-18' AS Date), CAST(N'2023-03-20' AS Date))
GO
INSERT [dbo].[findhelper] ([FindHelperid], [FindHelperName], [FindHelperEmail], [FindHelperImg], [FindHelperPhone], [FindHelperAddress], [FindHelperDescription], [FindHelperLocation]) VALUES (N'Find043468ba-91e4-4d43-97c4-7e2d0046ffb0', N'Peter', N'quangquyen1304@gmail.com', N'person_2.jpg', N'0999123456', N'105 Hồ Xuân Hương, Ngũ Hành Sơn, Đà Nẵng', NULL, N'Đà Nẵng')
INSERT [dbo].[findhelper] ([FindHelperid], [FindHelperName], [FindHelperEmail], [FindHelperImg], [FindHelperPhone], [FindHelperAddress], [FindHelperDescription], [FindHelperLocation]) VALUES (N'Find04394508-25e6-4b88-88ad-04e7c4135e2f', N'NguyenVanC', N'NguyenVanC@gmail.com', N'user.png', N'0999635253', N'Bến Ninh Kiều, Thành Phố Cần Thơ', N'Bản thân là 1 người hòa đồng vui tính, dễ gần gũi và sống rất tốt với mọi người.', N'Cần Thơ')
INSERT [dbo].[findhelper] ([FindHelperid], [FindHelperName], [FindHelperEmail], [FindHelperImg], [FindHelperPhone], [FindHelperAddress], [FindHelperDescription], [FindHelperLocation]) VALUES (N'Findd9c5a50b-4682-4bf6-922c-5f89a2836b8b', N'Tuyển dụng 1', N'tuyendung1@gmail.com', N'person_1.jpg', N'0147852964', N'Nguyễn Văn Cừ, Quận 10 , TP Hồ Chí Minh', N'Gia đình tôi là người TP. Hồ Chí Minh gồm 2 vợ chồng tôi và 1 trẻ em.', N'TP.Hồ Chí Minh')
GO
INSERT [dbo].[job] ([Jobid], [JobName], [JobType], [JobStart], [JobStartFlexibility], [JobDatePost], [JobGender], [JobEducation], [JobAge], [JobExp], [JobSalaryMin], [JobSalryMax], [JobImage], [JobTitle], [JobDescription], [FindHelperid]) VALUES (N'Job59dcb450-4110-47ca-a51b-6028541c733f', N'Làm việc nhà và nấu ăn mỗi ngày', N'Full Time', CAST(N'2023-07-29' AS Date), N'20/7/2023 12:00:00 AM', CAST(N'2023-03-23' AS Date), N'Bất kì', N'Không Yêu Cầu', N'Từ 30 đến 40 tuổi', N'3-40 năm', N'10.000.000', N'15.000.000', N'chamsoctreem.jpg', N'Cần tìm người giúp việc làm việc nhà và nấu ăn ', N'Chúng tôi đang tìm một người giúp việc gia đình ở TP. Hồ Chí Minh, công việc bao gồm chăm sóc một đứa trẻ 3 tuổi, dọn dẹp nhà cửa và nấu bữa sáng, bữa trưa và bữa tối. Ứng viên cần phải có tình yêu và sự kiên nhẫn với trẻ em.', N'Findd9c5a50b-4682-4bf6-922c-5f89a2836b8b')
INSERT [dbo].[job] ([Jobid], [JobName], [JobType], [JobStart], [JobStartFlexibility], [JobDatePost], [JobGender], [JobEducation], [JobAge], [JobExp], [JobSalaryMin], [JobSalryMax], [JobImage], [JobTitle], [JobDescription], [FindHelperid]) VALUES (N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2', N'Làm việc nhà và nấu ăn mỗi ngày', N'Full Time', CAST(N'2023-07-30' AS Date), N'23/7/2023 12:00:00 AM', CAST(N'2023-03-23' AS Date), N'Bất kì', N'Không Yêu Cầu', N'Từ 30 đến 40 tuổi', N'3-40 năm', N'10.000.000', N'15.000.000', N'bg_1.jpg', N'Cần tìm người giúp việc làm việc nhà và nấu ăn ', N'Chúng tôi đang tìm một người giúp việc gia đình ở TP. Hồ Chí Minh, công việc bao gồm chăm sóc một đứa trẻ 3 tuổi, dọn dẹp nhà cửa và nấu bữa sáng, bữa trưa và bữa tối. Ứng viên cần phải có tình yêu và sự kiên nhẫn với trẻ em.', N'Findd9c5a50b-4682-4bf6-922c-5f89a2836b8b')
INSERT [dbo].[job] ([Jobid], [JobName], [JobType], [JobStart], [JobStartFlexibility], [JobDatePost], [JobGender], [JobEducation], [JobAge], [JobExp], [JobSalaryMin], [JobSalryMax], [JobImage], [JobTitle], [JobDescription], [FindHelperid]) VALUES (N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18', N'Chăm sóc trẻ em nhỏ tuổi (5 tuổi) và 1 con chó', N'Full Time', CAST(N'2023-03-11' AS Date), N'15/3/2023 12:00:00 AM', CAST(N'2023-03-11' AS Date), N'Nữ', N'Tốt nghiệp THPT', N'Từ 30 đến 50 ', N'Lớn hơn 5 năm kinh nghiệm', N'5.000.000', N'10.000.000', N'chamsoctreem.jpg', N'Cần tuyển gấp người chăm trẻ em và chó', N'Xin chào! chúng tôi là một gia đình 4 người. Chúng tôi có một đứa con 5 tuổi và một con chó.  Chúng tôi đang tìm một người đáng tin cậy và kiên nhẫn, người sẽ giúp chúng tôi chăm sóc em bé và con chó của chúng tôi, cũng như làm các công việc nhà cơ bản. Ngày bắt đầu dự kiến ​​là ngày 11 tháng 5, nhưng chúng tôi linh hoạt bạn có thể đi làm vào ngày 15 tháng 5. Chúng tôi không cung cấp chỗ ở trực tiếp nhưng chúng tôi sẽ trả tiền nhà ở và phương tiện đi lại. Nếu bạn quan tâm hoặc cần thêm thông tin, xin vui lòng cho tôi biết.', N'Find04394508-25e6-4b88-88ad-04e7c4135e2f')
INSERT [dbo].[job] ([Jobid], [JobName], [JobType], [JobStart], [JobStartFlexibility], [JobDatePost], [JobGender], [JobEducation], [JobAge], [JobExp], [JobSalaryMin], [JobSalryMax], [JobImage], [JobTitle], [JobDescription], [FindHelperid]) VALUES (N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa', N'Chăm sóc  1 con chó và làm việc nhà', N'Part Time', CAST(N'2023-03-12' AS Date), N'14/3/2023 12:00:00 AM', CAST(N'2023-03-12' AS Date), N'Bất kì', N'Tốt nghiệp Cao Đẳng, Đại Học', N'Từ 20 đến 40', N'Có kinh nghiệm nuôi thú cưng', N'10.000.000', N'15.000.000', N'sq_img_1.jpg', N'Cần tuyển gấp người chăm chó và làm việc nhà bán thời gian', N'Tôi là một chuyên gia công nghệ người Mỹ đến Việt Nam năm ngoái. Tôi có một con chó, Bruno, mà tôi đang tìm kiếm sự giúp đỡ. Tôi đang tìm một người trợ giúp bán thời gian đã có trụ sở tại đây. Tôi sống ở Đà Nẵng. Tôi muốn tìm sự giúp đỡ với các nhiệm vụ sau: 

Nhiệm vụ: 

khoảng 6 giờ sáng dắt chó đi dạo và cho ăn
khoảng 6 giờ chiều dắt chó đi dạo và cho ăn
cuối tuần dọn dẹp, giặt giũ và nấu ăn
Yêu cầu:

chăm sóc chó
cửa hàng tạp hóa
Nấu nướng
Làm sạch
Nhiệm vụ trong nước
Tiếng Anh
Các yếu tố tùy chọn:

Chúng tôi có thể sắp xếp nếu bạn muốn sống trong hoặc sống ra ngoài. Tôi có một phòng ngủ thứ hai đầy đủ và nếu chúng tôi sắp xếp, bạn có thể ở lại để có thể chăm sóc con chó và giúp đỡ nhiều hơn trong tuần.
Ngày nghỉ của bạn có thể linh hoạt.
Lượng thời gian bạn dành trong tuần để làm việc cho tôi có thể bị giới hạn, tùy thuộc vào tình trạng sẵn có của bạn.
Dựa trên những điều trên, chúng ta có thể thảo luận về mức lương của bạn, v.v.', N'Find043468ba-91e4-4d43-97c4-7e2d0046ffb0')
GO
INSERT [dbo].[jobseeker] ([JobSeekerid], [JobSeekername], [JobSeekerdob], [JobSeekerphone], [JobSeekergender], [JobSeekeraddress], [JobSeekerimg], [JobSeekerSalaryFrom], [JobSeekerSalaryTo], [JobSeekerDescription], [JobSeekerEmail], [JobSeekerLocation], [JobSeekerCVid]) VALUES (N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255', N'Quang Quyền', CAST(N'1999-05-13' AS Date), N'0867130599', 0, N'Ngũ Hành Sơn, Đà Nẵng', N'person_4.jpg', N'10.000.000', N'15.000.000', N'vui vẻ ', N'quangquyen1305@gmail.com', N'Đà Nẵng', N'Cv1ef05ca6-0ef4-499f-9108-9a770dc22952')
INSERT [dbo].[jobseeker] ([JobSeekerid], [JobSeekername], [JobSeekerdob], [JobSeekerphone], [JobSeekergender], [JobSeekeraddress], [JobSeekerimg], [JobSeekerSalaryFrom], [JobSeekerSalaryTo], [JobSeekerDescription], [JobSeekerEmail], [JobSeekerLocation], [JobSeekerCVid]) VALUES (N'Job5bc6d9a8-78ea-46f3-aa2a-ddb38d033bda', N'Quang Quyền', CAST(N'2002-02-12' AS Date), N'0789456123', 0, N'Ngũ Hành Sơn, Hà Nội', N'person_3.jpg', NULL, NULL, N'Tên tôi là Quang Quyền, tôi 21 tuổi, độc thân. Tôi đến từ Hà Nội. Tôi đã làm giúp việc gia đình được 5 năm tại Hà Nội, họ là 2 người lớn với thú cưng. Tôi có thể chuyển bất cứ lúc nào. Tôi đang tìm một chủ nhân mới. Tôi có kinh nghiệm nấu ăn, tiếp thị, làm tất cả các công việc gia đình nói chung và chăm sóc trẻ em. Cảm ơn.', N'quangquyen1306@gmail.com', N'Hà Nội', NULL)
INSERT [dbo].[jobseeker] ([JobSeekerid], [JobSeekername], [JobSeekerdob], [JobSeekerphone], [JobSeekergender], [JobSeekeraddress], [JobSeekerimg], [JobSeekerSalaryFrom], [JobSeekerSalaryTo], [JobSeekerDescription], [JobSeekerEmail], [JobSeekerLocation], [JobSeekerCVid]) VALUES (N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', N'Nguyen Van A', CAST(N'1999-03-04' AS Date), N'0966851837', 0, N'Ngũ Hành Sơn, Đà Nẵng', N'person_5.jpg', N'5000000', N'10000000', N'Tôi là Nguyen Van A, 24 tuổi, đã kết hôn và có 1 con đến từ Đà Nẵng. Tôi làm giúp việc gia đình ở Hà Nội hơn 3 năm. Tôi đang ở trong 2 tháng với chủ nhân hiện tại của tôi là 1 người lớn tuổi. Tôi làm việc nhà, nấu ăn, tiếp thị, chăm sóc trẻ em và người già. Tôi có tổ chức cao, có trách nhiệm và đáng tin cậy. Tôi có thể chuyển nhượng vào ngày 20 tháng 3 năm 2023. Xin cảm ơn.', N'NguyenVanA@gmail.com', N'Đà Nẵng', N'Cve0e166bc-719f-48a9-a18f-ee0cbbee6394')
GO
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskill016e446e-b3c2-4ab8-89ab-dbf1d08c1f9a', N'Món Chay', N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255', NULL)
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskill0e07c14b-a54a-40d5-8f92-9071e0037659', N'Nấu đồ ăn cho trẻ em', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskill2d1191d4-89c0-4aa4-ba92-428e010986fd', N'Món Chay', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskill5fc6f078-bec8-4816-9bc7-96551ef1021a', N'Món Mặn', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskill6a616501-c4c2-407b-ad15-080331a71483', N'Món Mặn', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskillae5d85ed-2c1a-4579-bd39-67bc22a67255', N'Món Mặn', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskilld9614e4c-a6cb-4a24-bb1e-f9d7f0a68c89', N'Nấu đồ ăn cho trẻ em', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskille5257e7e-d41d-4878-8e2d-730a9bccad08', N'Nấu đồ ăn cho trẻ em', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekercookingskill] ([JobSeekerCookingSkillid], [JobSeekerCookingSkillname], [JobSeekerid], [Jobid]) VALUES (N'cookingskillf6146283-ab60-44d4-beee-27ddce2d22a9', N'Món Mặn', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
GO
INSERT [dbo].[jobseekerlanguage] ([JobSeekerLanguageid], [JobSeekerLanguagename], [JobSeekerid]) VALUES (N'languageskill1ab053a0-518b-4756-aa79-ba40d6791e23', N'Chưa tốt nghiệp', N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255')
INSERT [dbo].[jobseekerlanguage] ([JobSeekerLanguageid], [JobSeekerLanguagename], [JobSeekerid]) VALUES (N'languageskillb3492550-b173-4bd9-8b72-fb040a959266', N'Tốt nghiệp Cao Đẳng, Đại Học', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09')
GO
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill0ac1c0db-55b7-468f-a150-c89ea9141c99', N'Dọn dẹp nhà cửa', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill135e70aa-c05d-4d85-8d0f-b8e19d13e1aa', N'Chăm sóc em bé', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill15ef3b3b-b6b4-4bac-adac-3bd0121fed48', N'Chăm sóc em bé', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill225388b3-cc04-49ad-bd5f-b423da3a9fa4', N'Nấu Ăn', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill2880b3c5-305c-422a-8c8d-92c0602db374', N'Chăm sóc em bé', N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255', NULL)
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill2b52e184-95dc-4449-b902-8652b4bf844f', N'Dọn dẹp nhà cửa', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill2f82fa45-8aa6-4216-ac04-3f16676c8b48', N'Chăm sóc thú nuôi', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill427153ac-21c6-4764-807c-a550d9fd156f', N'Nấu Ăn', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill59a79dbc-e6e8-46e6-8336-237503ce58fd', N'Chăm sóc em bé', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill6f21d8bc-5134-4656-aae0-e8a56b2c6d41', N'Nấu Ăn', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill7a2561fd-4b43-4911-b299-099e66c2fbcd', N'Chăm sóc thú nuôi', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill912812c3-3007-4483-9d20-94d0d5720bf4', N'Đi chợ', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill9c7cadda-ed87-4abb-8168-45df1ed98512', N'Dọn dẹp nhà cửa', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskill9d2bdc78-252e-4633-88c7-acb79463e6a0', N'Lái xe', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskillcf15250e-b113-48f8-abd0-a67ed50b4379', N'Đi chợ', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskilld03c9450-d396-46d7-a5bd-ae22f7eba928', N'Dọn dẹp nhà cửa', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskilldf3270eb-011c-4981-9231-7100568b1cd8', N'Nấu Ăn', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskille1423b80-e098-4474-8577-61b37923ac69', N'Đi chợ', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekermainskill] ([JobSeekerMainSkillid], [JobSeekerMainSkillname], [JobSeekerid], [Jobid]) VALUES (N'mainskillf9d5fd13-204b-45ee-90a7-47a68a55cdc2', N'Nấu Ăn', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
GO
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill1ea3f96f-3371-44f3-962b-9c163f020411', N'Làm vườn', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill2cb60dbd-f523-4110-847e-3183afab9363', N'Sơ Cứu', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill374c5562-0c96-475d-a1ab-a171ecf35ba6', N'Làm bánh', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill44b6e9e7-1f73-4edc-8dc7-acaa2de977fb', N'Sơ Cứu', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill5a55461d-4d23-44a1-9bfd-96865585d17e', N'Làm bánh', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill65f5a817-90b5-4ee9-8535-4e25964392d3', N'Làm bánh', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill76c3c92e-94aa-4dfe-8670-491d06627e6a', N'Làm vườn', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill7a479625-3f1a-4ae6-8533-09a65bbade05', N'May Vá', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill7f6da538-a5bf-4eda-b8b8-88481c7161ea', N'Làm bánh', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskill802dcf65-8e89-428e-a7c5-ea9a4b6d6e86', N'Sơ Cứu', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskillbbcf05d1-059e-4469-a204-0b60cd119683', N'Làm vườn', NULL, N'Jobecaa0c42-c3ba-45dc-901a-2269a35b56aa')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskilldc6f5454-caa9-4c19-b7f4-b6c3b3c2849d', N'Sơ Cứu', N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', NULL)
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskilldff3ff00-f537-453a-a3ed-57d054b88457', N'Làm bánh', NULL, N'Job59dcb450-4110-47ca-a51b-6028541c733f')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskille5fddb06-615e-4322-8e00-12b0f959e7e6', N'Sơ Cứu', NULL, N'Job5d723fc2-ab19-404f-aaf9-68ee1b4222d2')
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskillf0782918-6736-434d-a413-36040a967740', N'Làm bánh', N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255', NULL)
INSERT [dbo].[jobseekerotherskill] ([JobSeekerOtherSkillid], [JobSeekerOtherSkillname], [JobSeekerid], [Jobid]) VALUES (N'otherskillf29e5b96-17b3-4d66-8a1b-a4cefd1d6a6d', N'May Vá', NULL, N'Jobc9738a30-d5f2-40bc-9030-fa49848edc18')
GO
INSERT [dbo].[role] ([RoleId], [RoleName]) VALUES (1, N'Admins')
INSERT [dbo].[role] ([RoleId], [RoleName]) VALUES (2, N'JobSeeker')
INSERT [dbo].[role] ([RoleId], [RoleName]) VALUES (3, N'FindHelper')
GO
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Admin', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', 1, N'Admin@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Find043468ba-91e4-4d43-97c4-7e2d0046ffb0', N'quangquyen', N'e10adc3949ba59abbe56e057f20f883e', 3, N'quangquyen1304@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Find04394508-25e6-4b88-88ad-04e7c4135e2f', N'NguyenVanC', N'e10adc3949ba59abbe56e057f20f883e', 3, N'NguyenVanC@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Findd9c5a50b-4682-4bf6-922c-5f89a2836b8b', N'tuyendung1', N'e10adc3949ba59abbe56e057f20f883e', 3, N'tuyendung1@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Job06bafe6d-b880-432a-84e0-cb6bfa2d6255', N'quyentq1', N'827ccb0eea8a706c4c34a16891f84e7b', 2, N'quangquyen1305@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Job5bc6d9a8-78ea-46f3-aa2a-ddb38d033bda', N'quyentq2', N'1d3781eb8c7e4b0d333caa981617b249', 2, N'quangquyen1306@gmail.com')
INSERT [dbo].[users] ([UserID], [UserName], [Passwords], [RoleId], [Email]) VALUES (N'Jobcc1b7532-ce6d-4067-a44c-0772b5544f09', N'NguyenvanA', N'1d3781eb8c7e4b0d333caa981617b249', 2, N'NguyenVanA@gmail.com')
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Adname]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Addob]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Adphone]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Adgener]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Adaddress]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Ademail]
GO
ALTER TABLE [dbo].[admin] ADD  DEFAULT (NULL) FOR [Adimg]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperName]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperEmail]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperImg]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperPhone]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperAddress]
GO
ALTER TABLE [dbo].[findhelper] ADD  DEFAULT (NULL) FOR [FindHelperDescription]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobName]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobType]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobStart]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobStartFlexibility]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobDatePost]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobGender]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobEducation]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobAge]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobExp]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobSalaryMin]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobSalryMax]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobImage]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobTitle]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [JobDescription]
GO
ALTER TABLE [dbo].[job] ADD  DEFAULT (NULL) FOR [FindHelperid]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekername]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerdob]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerphone]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekergender]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekeraddress]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerimg]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerSalaryFrom]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerSalaryTo]
GO
ALTER TABLE [dbo].[jobseeker] ADD  DEFAULT (NULL) FOR [JobSeekerDescription]
GO
ALTER TABLE [dbo].[jobseekercookingskill] ADD  DEFAULT (NULL) FOR [JobSeekerCookingSkillname]
GO
ALTER TABLE [dbo].[jobseekercookingskill] ADD  DEFAULT (NULL) FOR [JobSeekerid]
GO
ALTER TABLE [dbo].[jobseekerlanguage] ADD  DEFAULT (NULL) FOR [JobSeekerLanguagename]
GO
ALTER TABLE [dbo].[jobseekerlanguage] ADD  DEFAULT (NULL) FOR [JobSeekerid]
GO
ALTER TABLE [dbo].[jobseekermainskill] ADD  DEFAULT (NULL) FOR [JobSeekerMainSkillname]
GO
ALTER TABLE [dbo].[jobseekermainskill] ADD  DEFAULT (NULL) FOR [JobSeekerid]
GO
ALTER TABLE [dbo].[jobseekerotherskill] ADD  DEFAULT (NULL) FOR [JobSeekerOtherSkillname]
GO
ALTER TABLE [dbo].[jobseekerotherskill] ADD  DEFAULT (NULL) FOR [JobSeekerid]
GO
ALTER TABLE [dbo].[role] ADD  DEFAULT (NULL) FOR [RoleName]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [UserName]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [Passwords]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [RoleId]
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD FOREIGN KEY([CVid])
REFERENCES [dbo].[CV] ([CVid])
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD FOREIGN KEY([FindHelperid])
REFERENCES [dbo].[findhelper] ([FindHelperid])
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD FOREIGN KEY([Jobid])
REFERENCES [dbo].[job] ([Jobid])
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[jobseeker] ([JobSeekerid])
GO
ALTER TABLE [dbo].[findhelper]  WITH CHECK ADD  CONSTRAINT [FK_findhelper_Users] FOREIGN KEY([FindHelperid])
REFERENCES [dbo].[users] ([UserID])
GO
ALTER TABLE [dbo].[findhelper] CHECK CONSTRAINT [FK_findhelper_Users]
GO
ALTER TABLE [dbo].[job]  WITH CHECK ADD  CONSTRAINT [FK_FindHelperid] FOREIGN KEY([FindHelperid])
REFERENCES [dbo].[findhelper] ([FindHelperid])
GO
ALTER TABLE [dbo].[job] CHECK CONSTRAINT [FK_FindHelperid]
GO
ALTER TABLE [dbo].[jobseeker]  WITH CHECK ADD FOREIGN KEY([JobSeekerCVid])
REFERENCES [dbo].[CV] ([CVid])
GO
ALTER TABLE [dbo].[jobseeker]  WITH CHECK ADD  CONSTRAINT [FK_jobseeker_Users] FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[users] ([UserID])
GO
ALTER TABLE [dbo].[jobseeker] CHECK CONSTRAINT [FK_jobseeker_Users]
GO
ALTER TABLE [dbo].[jobseekercookingskill]  WITH CHECK ADD FOREIGN KEY([Jobid])
REFERENCES [dbo].[job] ([Jobid])
GO
ALTER TABLE [dbo].[jobseekercookingskill]  WITH CHECK ADD  CONSTRAINT [FK_JobSeekerCookingSkill] FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[jobseeker] ([JobSeekerid])
GO
ALTER TABLE [dbo].[jobseekercookingskill] CHECK CONSTRAINT [FK_JobSeekerCookingSkill]
GO
ALTER TABLE [dbo].[jobseekerlanguage]  WITH CHECK ADD  CONSTRAINT [FK_JobSeekerid] FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[jobseeker] ([JobSeekerid])
GO
ALTER TABLE [dbo].[jobseekerlanguage] CHECK CONSTRAINT [FK_JobSeekerid]
GO
ALTER TABLE [dbo].[jobseekermainskill]  WITH CHECK ADD FOREIGN KEY([Jobid])
REFERENCES [dbo].[job] ([Jobid])
GO
ALTER TABLE [dbo].[jobseekermainskill]  WITH CHECK ADD  CONSTRAINT [FK_JobSeekerMainSkill] FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[jobseeker] ([JobSeekerid])
GO
ALTER TABLE [dbo].[jobseekermainskill] CHECK CONSTRAINT [FK_JobSeekerMainSkill]
GO
ALTER TABLE [dbo].[jobseekerotherskill]  WITH CHECK ADD FOREIGN KEY([Jobid])
REFERENCES [dbo].[job] ([Jobid])
GO
ALTER TABLE [dbo].[jobseekerotherskill]  WITH CHECK ADD  CONSTRAINT [FK_JobSeekerOtherSkill] FOREIGN KEY([JobSeekerid])
REFERENCES [dbo].[jobseeker] ([JobSeekerid])
GO
ALTER TABLE [dbo].[jobseekerotherskill] CHECK CONSTRAINT [FK_JobSeekerOtherSkill]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[role] ([RoleId])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_RoleId]
GO
