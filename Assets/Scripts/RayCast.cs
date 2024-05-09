using UnityEngine;

public class RayCast : MonoBehaviour
{
    RaycastHit2D hit;

    // Оголошуємо тег, який ми будемо перевіряти
    public string targetTag = "Finish";

    // Оновлення викликається раз на кадр
    void Update()
    {
        // Визначаємо максимальну відстань
        float maxDistance = 5f;

        // Здійснюємо променеве випромінювання (лучемі) у право від позиції об'єкта
        hit = Physics2D.Raycast(transform.position, Vector2.right, maxDistance);

        // Візуалізуємо луч
        Debug.DrawRay(transform.position, Vector2.right * maxDistance, Color.green);

        // Перевіряємо, чи зіткнувся луч з об'єктом з певним тегом
        if (hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            Debug.Log(hit.collider.gameObject.name + " hit");
        }
    }
}
