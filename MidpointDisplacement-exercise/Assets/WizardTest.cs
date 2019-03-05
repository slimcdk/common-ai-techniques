using UnityEditor;
using UnityEngine;

public class WizardTest : ScriptableWizard
{
    public float someFloatVariable = 500;
    public string someStringVariable = "something";

    [MenuItem("Wizards/Test")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<WizardTest>("Wizard Title", "Create", "Apply");
        //If you don't want to use the secondary button simply leave it out:
        //ScriptableWizard.DisplayWizard<WizardTest>("Wizard Title", "Create");
    }

    void OnWizardCreate()
    {
        //Do something when the user clicks the "Create" button
        //Note: you can change the name of the "Create" button, but this function doesn't change
    }

    void OnWizardUpdate()
    {
        //Do something when the user changes something in the wizard
    }
    
    void OnWizardOtherButton()
    {
        //Do something when the user clicks the "Apply" button
        //Note: you can change the name of the "Apply" button, but this function doesn't change
    }
}