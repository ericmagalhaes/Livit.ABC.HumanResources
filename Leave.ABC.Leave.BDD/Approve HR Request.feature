Feature: Approve HR Request
	In order to approve HR requests
	As a manager
	I want to approve each HR request

@mytag
Scenario: approve an HR request
	Given I have received an HR approval request
	And I have reviewed the HR request
	When I press approve
	Then the result should be an approved HR request

Scenario: reject an HR request
	Given I have received an HR approval request
	And I have reviewed the HR request
	When I press reject
	Then the result should be a rejected HR request
