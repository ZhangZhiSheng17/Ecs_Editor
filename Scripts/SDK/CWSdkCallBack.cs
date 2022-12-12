using UnityEngine;

public class CWSdkCallBack : MonoBehaviour
{
    public void onImagePath(string imgname)
    {
       string imglocal =  Application.dataPath + "/" + imgname;
       
    }


    public void PubCallBack(string data)
    {
        string[] datas = data.Split('|');
        switch (datas[0])
        {
            case "1":
                //登陆回调
                LoginData logindata = JsonUtility.FromJson<LoginData>(datas[1]);


                break;
            case "2":
                //支付回调
                PayData paydata = JsonUtility.FromJson<PayData>(datas[1]);


                break;
            default:
                break;
        }
    }
}
