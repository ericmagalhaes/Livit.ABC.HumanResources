Feature: approvment of requests
	In order to approve only requests authorized by the manager
	As a manager
	I want to be warned when a new request comes from an employee

@mytag
Scenario: A request that needs approval must be warned to the responsable manager
	Given I have been warned of an approval request
	And I have approved 
	When I press approved
	Then the result should be an approved request
