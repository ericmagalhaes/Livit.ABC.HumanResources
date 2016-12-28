Feature: Leave request
	In order to schedule my leave 
	As an employee
	I want a scheduled event in my personal calendar

@mytag
Scenario: schedule a leave
	Given I am an employee
	And I have entered the start date and the end date of a new leave
	When I press add
	Then the result should be a scheduled leave in personal calendar

Scenario: reschedule a leave
	Given I have selected an existing scheduled leave
	And I have updated the start date, end date or both
	When I press reschedule
	Then the result should be an updated scheduled leave

Scenario: remove a scheduled leave
	Given I have selected an exising scheduled leave
	When I press remove
	Then the result should be a canceled leave

Scenario: retrieve a scheduled a leave
	Given I have entered a scheduled leave identity
	When I press retrieve
	Then the result should be a scheduled leave with the same identity
