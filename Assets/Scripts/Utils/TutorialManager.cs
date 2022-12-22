using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public void ElfSpawned()
    {
        if (PlayerPrefs.GetInt("FirstTimeElf", 0) == 0)
        {

            Utility.instance.ShowHelpText(
                new()
                {
                    "Look! an angry elf, you can click on him to throw a snowball. Don't forget to press enter before shooting"
                }, true, () => Utility.instance.SetPause(false));

            Utility.instance.SetPause(true);

            PlayerPrefs.SetInt("FirstTimeElf", 1);
        }
    }

    public void GiftDropped()
    {
        if (PlayerPrefs.GetInt("FirstTimeGift", 0) == 0)
        {

            Utility.instance.ShowHelpText(
                new()
                {
                    "There you go, see that wasn't so hard, was it ?",
                    "Anyways, in the top right of your screen you have a gifts counter",
                    "This increases every time you drop a gift, once you drop the required amount...",
                    "Of gifts, you get a candy cane as shown on the top left of your screen",
                    "As you might have noticed, you can't fly and this is for a reason...",
                    "As you remember you rushed out of the north pole to deliver gifts but...",
                    "You did not bring enough candy canes to feed to your reindeers",
                    "Candy canes are essential for your reindeers to fly",
                    "Once you have enough candy canes, you can end the run and...",
                    "Return to the north pole and deal with the angry elves",
                    "We'll talk more about ending the run later on"
                }, true, () => Utility.instance.SetPause(false));

            Utility.instance.SetPause(true);

            PlayerPrefs.SetInt("FirstTimeGift", 1);
        }

    }

    public void EndEnabled()
    {
        if (PlayerPrefs.GetInt("FirstTimeEnd", 0) == 0)
        {

            Utility.instance.ShowHelpText(
                new()
                {
                    "See that big yellow end run button ?",
                    "After you collect the required amount of candy canes, you can end the run",
                    "You can either press it or continue playing. The choice is up to you"
                }, true, () => Utility.instance.SetPause(false));

            Utility.instance.SetPause(true);

            PlayerPrefs.SetInt("FirstTimeEnd", 1);
        }
    }
}