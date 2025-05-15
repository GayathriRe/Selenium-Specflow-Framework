Feature: Login

A short summary of the feature

@tag1
Scenario: TC01 Login to application
	Given you have launched the application
	When you login to the application
		| username      | password     |
		| standard_user | secret_sauc |
    Then the home page opens successfully
       
