﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Livit.ABC.Leave.BDD
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AbsenceRequestFeature : Xunit.IClassFixture<AbsenceRequestFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AbsenceRequestSpec.feature"
#line hidden
        
        public AbsenceRequestFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Absence request", "\tIn order to schedule my absence \r\n\tAs an employee\r\n\tI want a scheduled event in " +
                    "my personal calendar", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(AbsenceRequestFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="schedule an absence")]
        [Xunit.TraitAttribute("FeatureTitle", "Absence request")]
        [Xunit.TraitAttribute("Description", "schedule an absence")]
        [Xunit.TraitAttribute("Category", "scheduleanabsence")]
        public virtual void ScheduleAnAbsence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("schedule an absence", new string[] {
                        "scheduleanabsence"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I am an employee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("I have entered the start date and the end date of a new absence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.When("I press add", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then("the result should be a scheduled absence in personal calendar", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="reschedule an absence")]
        [Xunit.TraitAttribute("FeatureTitle", "Absence request")]
        [Xunit.TraitAttribute("Description", "reschedule an absence")]
        [Xunit.TraitAttribute("Category", "rescheduleanabsence")]
        public virtual void RescheduleAnAbsence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("reschedule an absence", new string[] {
                        "rescheduleanabsence"});
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I have selected an existing scheduled absence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.And("I have updated the start date, end date or both", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.When("I press reschedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("the result should be an updated scheduled absence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="remove a scheduled absence")]
        [Xunit.TraitAttribute("FeatureTitle", "Absence request")]
        [Xunit.TraitAttribute("Description", "remove a scheduled absence")]
        [Xunit.TraitAttribute("Category", "removeanscheduleabsence")]
        public virtual void RemoveAScheduledAbsence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("remove a scheduled absence", new string[] {
                        "removeanscheduleabsence"});
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("I have selected an exising scheduled absence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.When("I press remove", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 24
 testRunner.Then("the result should be a canceled absence", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="retrieve a scheduled absence")]
        [Xunit.TraitAttribute("FeatureTitle", "Absence request")]
        [Xunit.TraitAttribute("Description", "retrieve a scheduled absence")]
        [Xunit.TraitAttribute("Category", "retrieveascheduleabsence")]
        public virtual void RetrieveAScheduledAbsence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("retrieve a scheduled absence", new string[] {
                        "retrieveascheduleabsence"});
#line 27
this.ScenarioSetup(scenarioInfo);
#line 28
 testRunner.Given("I have entered a scheduled absence identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.When("I press retrieve", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("the result should be a scheduled absence with the same identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="list scheduled absences")]
        [Xunit.TraitAttribute("FeatureTitle", "Absence request")]
        [Xunit.TraitAttribute("Description", "list scheduled absences")]
        [Xunit.TraitAttribute("Category", "listscheduleabsence")]
        public virtual void ListScheduledAbsences()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("list scheduled absences", new string[] {
                        "listscheduleabsence"});
#line 33
this.ScenarioSetup(scenarioInfo);
#line 34
 testRunner.Given("I have selected list all", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 35
 testRunner.When("I press list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("the result should be a list of scheduled absences", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AbsenceRequestFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AbsenceRequestFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
