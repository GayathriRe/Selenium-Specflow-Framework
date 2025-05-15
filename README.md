# Selenium SpecFlow Automation Framework

This is a robust and scalable test automation framework built using *Selenium WebDriver, **SpecFlow, and **C#. It is designed to support **data-driven testing, **cross-browser execution, and **screenshot capture on failure*, with clean architecture following best practices in automation.

---

## 🚀 Features

- ✅ BDD-style test cases using *SpecFlow*
- 🌐 Browser support: Chrome, Firefox, Edge (via DriverManager)
- 📋 Test data input from *Excel file*
- 🔍 Screenshot capture on test step failure
- 🔄 Hooks for setup and teardown
- 📂 Page Object Model for clean test code
- 🧪 Supports manual and CI execution (Azure DevOps-ready)

---

## 📁 Project Structure
├── Features/             # Feature files written in Gherkin
├── StepDefinitions/      # Step definitions mapped to features
├── Pages/                # Page Object classes
├── Utilities/            # Helper classes (e.g., DriverManager, ExcelReader, ScreenshotHelper)
├── Hooks/                # Before/After scenario hooks
├── Screenshots/          # Failure screenshots are saved here
├── TestData.xlsx         # Excel file with test credentials
---

## 🧪 Sample Test Flow

```gherkin
**Scenario1**: TC01 Login to application
	Given you have launched the application
	When you login to the application
		| username      | password     |
		| standard_user | secret_sauce |
    Then the home page opens successfully
**Scenario2**: Successful Login using Excel 
  Given I launch the application
  When I login using valid credentials from Excel
  Then I should see the dashboard

📖 Technologies Used
	•	💻 Selenium WebDriver
	•	🧪 SpecFlow (Cucumber for .NET)
	•	⌨️ C#
	•	🗂 NPOI for reading Excel
	•	🧼 MSTest / NUnit (as test runner)
	•	📸 Screenshot capture with ITakesScreenshot
