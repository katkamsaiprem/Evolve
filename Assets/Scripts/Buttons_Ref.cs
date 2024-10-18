// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class Buttons_Ref : MonoBehaviour
// {
//     public Button Evolution_1_Button;
//     // public Image Evolution_1_Image;
//     // public Image Evolution_1_Lock;
//     // public Image Evolution_1_Unlock;
//     public Button Evolution_2_Button;
//     public Button Evolution_3_Button;
//     public GameManager_ GameManager_;
//
//     void Start()
//     {
//         // Make Evolution_1_Button interactable
//         Evolution_1_Button.interactable = true;
//         // Evolution_1_Lock.enabled = false;
//         
//         // Make Evolution_2_Button and Evolution_3_Button non-interactable  
//         Evolution_2_Button.interactable = false;
//         Evolution_3_Button.interactable = false;
//     }
//
//     
//
//     public void buton()
//     {
//
//         if (GameManager_.score >= GameManager_.required_Charge)
//         {
//             Evolution_1_Button.interactable = false;
//             Evolution_2_Button.interactable = true;
//             Evolution_3_Button.interactable = false;
//         }
//
//     }
//     
//   
// }
//
//
//
//   
