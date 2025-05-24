public class WebhookService
{
  private readonly List<Subscription> _subscriptions = new();
  private readonly HttpClient _httpClient = new();

  public void Subscribe(Subscription subscription)
  {
    _subscriptions.Add(subscription);
  }

  public async Task PublishMessage(string topic, object message)
  {
    var subscribeWebhooks = _subscriptions.Where(w => w.Topic == topic);

    foreach(var webhoook in subscribeWebhooks)
    {
      await _httpClient.PostAsJsonAsync(webhoook.Callback, message);
    }
  }
}