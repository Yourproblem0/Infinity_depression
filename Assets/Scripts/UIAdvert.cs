using TMPro;
using UnityEngine;

public class UIAdvert : MonoBehaviour
{
    [SerializeField] TMP_Text offerExpiresTextObject;
    [SerializeField] GameObject goodJobTextObject;
    [SerializeField] GameObject virusTextObject;
    [SerializeField] float offerDuration = 5f;
    private float offerTimer;
    private void Start()
    {
        //Set the timer to 5 sec
        offerTimer = offerDuration;
        //Turnoff the good job text
        goodJobTextObject.SetActive(false);
       
    }
    private void Update()
    {
        //reduce time from the offer timer
        offerTimer -= Time.deltaTime;

        //Check if the sale has ended
        if (offerTimer <= 0)
        {
            goodJobTextObject.SetActive(true);
            virusTextObject.SetActive(false);
            offerExpiresTextObject.alpha = 0;
        }

        //Update offer time text
        UpdateOfferTimerText(offerTimer);
    }

    private void UpdateOfferTimerText(float timeleft)
    {
        offerExpiresTextObject.text = $"Save your computer from handsome squidward! Press here in {timeleft:F2}";
    }
}
