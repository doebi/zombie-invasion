using UnityEngine;
using System.Collections;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

public class MQTTListener : MonoBehaviour {

    const string MQTT_BROKER_ADDRESS = "mqtt.devlol.org";

	// Use this for initialization
	void Start () {
        MqttClient client = new MqttClient(MQTT_BROKER_ADDRESS);
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        client.Connect("unity");
        client.Subscribe(new string[] { "doebi/watergun/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) {
        string msg = Encoding.UTF8.GetString(e.Message);
        string top = e.Topic;
        if (top == "doebi/watergun/trigger" && msg == "DOWN") {
            Debug.Log("PENG");            
        }
    }
}
