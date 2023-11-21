using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreList_group_Script : MonoBehaviour {

#region Props 
[SerializeField]  private int id;
[SerializeField]  private Image ScoreOwnerBG_image;
[SerializeField]  private TextMeshProUGUI ScoreOwner_text;
[SerializeField]  private Image ScoreCountFieldBG_image;
[SerializeField]  private TextMeshProUGUI ScoreCountField_text;
#endregion

#region Functions 
#endregion
void Awake() {
}
public void init(object initObject) {
}

public void setName(string name)
{
    ScoreOwner_text.text = name;
}

public void setScore(int score)
{
    ScoreCountField_text.text = "" + score;
}

    
}
