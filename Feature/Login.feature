Feature: Login

A short summary of the feature

@tag1
Scenario: TC01 Login to application using scenario outline
	Given you have launched the application
	When you login with "<username>" and "<password>"
	Then the home page opens successfully
Examples:
	| username                | password     |
	| standard_user           | secret_sauce |
	| problem_user            | secret_sauce |
	| performance_glitch_user | secret_sauce |
	| error_user              | secret_sauce |
	| visual_user             | secret_sauce |
       
Scenario: TC02 Login to application using excel data
	Given you have launched the application
	When I login using valid credentials from Excel
	Then the home page opens successfully


Scenario: TC03 Login to application using data table
	Given you have launched the application
	When you login with the credentials
		| username      | password     |
		| standard_user | secret_sauce |
	Then the home page opens successfully

Scenario: TC04 Login to application using config file
	Given you have launched the application
	When you login with the credentials using config file
	Then the home page opens successfully
