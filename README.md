# Mimo challenge
Basic version of the **Mimo** server functionality

## Endpoints
- **POST: /api/course/lesson**
	Post lesson to the server. Lesson will be added to the user with id 1.
	
	**Returns:** 
	Status code 201 on successful insert.
	Status code 500 on error thrown.
		
	
	*Example body:*
	>  {
	  "lessonId": 1,
	  "startDate": "2021-05-22T13:05:17.476Z",
	  "completionDate": "2021-05-22T13:05:17.476Z"
}

___

- **GET: /api/achievement/user/{userId}**
	Gets achievements with completion status for user.
	
	**Returns:** 
	Status code 200 on successful insert.
	Status code 500 on error thrown.
		
	
	*Example response:*
	>  [
  {
    "achievementId": 1,
    "name": "Complete 5 lessons",
    "completed": true,
    "progress": 7
  },
  {
    "achievementId": 4,
    "name": "Complete 1 chapter",
    "completed": true,
    "progress": 3
  },
  {
    "achievementId": 6,
    "name": "Complete the Swift course",
    "completed": true,
    "progress": 1
  },
  {
    "achievementId": 2,
    "name": "Complete 25 lessons",
    "completed": false,
    "progress": 7
  },
  {
    "achievementId": 3,
    "name": "Complete 50 lessons",
    "completed": false,
    "progress": 7
  },
  {
    "achievementId": 5,
    "name": "Complete 5 chapters",
    "completed": false,
    "progress": 3
  },
  {
    "achievementId": 7,
    "name": "Complete the Javascript course",
    "completed": false,
    "progress": 0
  },
  {
    "achievementId": 8,
    "name": "Complete the C# course",
    "completed": false,
    "progress": 0
  }
]


## Initial data
 - Users 
![Users](https://i.ibb.co/L5vBZNr/users.png)

 - Courses 
![Courses](https://i.ibb.co/Sy4C5pG/courses.png)
 
 

 - Chapters
![Chapters](https://i.ibb.co/cQ2FpsV/chapters.png)

 - Lessons
 ![Lessons](https://i.ibb.co/sgy39Gj/lessons.png)

 - Achievements
 ![enter image description here](https://i.ibb.co/jh7fVcj/achievements.png)
