using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

public class MQTTListener : MonoBehaviour {

    const string MQTT_BROKER_ADDRESS = "mqtt.devlol.org";
    private MqttClient client;

	// Use this for initialization
	void Start () {
        client = new MqttClient(MQTT_BROKER_ADDRESS);
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        client.Connect("unity");
        client.Subscribe(new string[] { "doebi/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) {
        string msg = Encoding.UTF8.GetString(e.Message);
        string top = e.Topic;
        if (top == "doebi/watergun/button" && msg == "DOWN") {
            Debug.Log("trigger down");
        }
        if (top == "doebi/watergun/button" && msg == "UP") {
            Debug.Log("trigger up");
        }
        if (top == "doebi/bullshit/button" && msg == "DOWN") {
            Debug.Log("button down");
        }
        if (top == "doebi/bullshit/button" && msg == "UP") {
            Debug.Log("button up");
        }
    }

    // let the gun vibrate
    public void vibrator_on() {
        client.Publish("doebi/watergun/vibrator", Encoding.UTF8.GetBytes("ON")); 
    }

    public void vibrator_off() {
        client.Publish("doebi/watergun/vibrator", Encoding.UTF8.GetBytes("OFF")); 
    }
}
