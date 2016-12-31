Feature: Absence request
	In order to schedule my absence 
	As an employee
	I want a scheduled event in my personal calendar

@scheduleanabsence
Scenario: schedule an absence
	Given I am an employee
	And I have entered the start date and the end date of a new absence
	When I press add
	Then the result should be a scheduled absence in personal calendar
	And the employee manager must be warned to approve the request
	

@rescheduleanabsence
Scenario: reschedule an absence
	Given I have selected an existing scheduled absence
	And I have updated the start date, end date or both
	When I press reschedule
	Then the result should be an updated scheduled absence
	And the employee`s manager must approve the updated schedule

@removeanscheduleabsence
Scenario: remove a scheduled absence
	Given I have selected an exising scheduled absence
	When I press remove
	Then the result should be a canceled absence
	And the employee`s manager must be warned

@retrieveascheduleabsence
Scenario: retrieve a scheduled absence
	Given I have entered a scheduled absence identity
	When I press retrieve
	Then the result should be a scheduled absence with the same identity

@listscheduleabsence
Scenario: list scheduled absences
	Given I have selected list all
	When I press list
	Then the result should be a list of scheduled absences