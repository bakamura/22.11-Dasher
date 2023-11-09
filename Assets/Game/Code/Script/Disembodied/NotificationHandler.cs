using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationHandler : MonoBehaviour {

    [SerializeField] private NotificationInfo[] _notifications;

    private void Start() {
        AndroidNotificationCenter.CancelAllNotifications(); // Clean Schedule + Already shown

        // Safe to instance AndroidNotificationChannel, as it's ignored when "id" already exists
        AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel(
            "dasher_id",
            "Dasher",
            "Notification Channel for the game Dasher",
            Importance.Default));

        foreach (NotificationInfo notification in _notifications) CreateAndroidNotification(notification);

        // Debug
        AndroidNotificationCenter.SendNotification(new AndroidNotification("Test Notification", "this is a test duh", System.DateTime.Now.AddSeconds(15.5)), "dasher_id");
    }

    private void CreateAndroidNotification(NotificationInfo info) {
        AndroidNotification notification = new AndroidNotification(info.title, info.message,
            System.DateTime.Today.AddDays(info.daysAhead).AddHours(info.hour));
        if (info.repeat) notification.RepeatInterval = TimeSpan.FromDays(info.daysAhead);

        AndroidNotificationCenter.SendNotification(notification, "dasher_id");
    }

}

[System.Serializable]
public class NotificationInfo {

    public string title;
    public string message;
    public int daysAhead;
    public int hour;
    public bool repeat;

}