using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform playerTransform; // مرجع للاعب
    
    [Header("Save Settings")]
    public string saveKey = "PlayerSaveData"; // مفتاح الحفظ
    
    [Header("Loading Settings")]
    public bool loadOnStart = false; // هل تريد التحميل التلقائي عند بدء اللعبة؟
    
    private bool isLoading = false; // لمنع التحميل المتكرر
    
    private void Start()
    {
        // إذا لم يتم تحديد مرجع اللاعب، ابحث عنه تلقائياً
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogWarning("لم يتم العثور على اللاعب! تأكد من وجود GameObject بـ Tag 'Player'");
            }
        }
        
        // التحميل التلقائي عند بدء اللعبة (اختياري)
        if (loadOnStart && HasSavedData())
        {
            LoadPlayerPosition();
        }
    }
    
    /// <summary>
    /// حفظ موقع اللاعب والمشهد الحالي
    /// </summary>
    public void SavePlayerPosition()
    {
        if (playerTransform == null)
        {
            Debug.LogError("مرجع اللاعب غير محدد!");
            return;
        }
        
        // حفظ اسم المشهد الحالي
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(saveKey + "_SceneName", currentSceneName);
        
        // حفظ الموقع
        Vector3 position = playerTransform.position;
        PlayerPrefs.SetFloat(saveKey + "_PosX", position.x);
        PlayerPrefs.SetFloat(saveKey + "_PosY", position.y);
        PlayerPrefs.SetFloat(saveKey + "_PosZ", position.z);
        
        // حفظ الدوران
        Vector3 rotation = playerTransform.eulerAngles;
        PlayerPrefs.SetFloat(saveKey + "_RotX", rotation.x);
        PlayerPrefs.SetFloat(saveKey + "_RotY", rotation.y);
        PlayerPrefs.SetFloat(saveKey + "_RotZ", rotation.z);
        
        // حفظ البيانات
        PlayerPrefs.Save();
        
        Debug.Log("تم حفظ موقع اللاعب في المشهد: " + currentSceneName + " - الموقع: " + position.ToString());
        
        // إظهار رسالة للمستخدم
        ShowMessage("تم الحفظ! المشهد: " + currentSceneName);
    }
    
    /// <summary>
    /// استعادة موقع اللاعب والمشهد المحفوظ
    /// </summary>
    public void LoadPlayerPosition()
    {
        if (isLoading)
        {
            Debug.LogWarning("عملية التحميل جارية بالفعل!");
            return;
        }
        
        // التحقق من وجود بيانات محفوظة
        if (!HasSavedData())
        {
            Debug.LogWarning("لا توجد بيانات محفوظة!");
            ShowMessage("لا توجد بيانات محفوظة!");
            return;
        }
        
        // الحصول على اسم المشهد المحفوظ
        string savedSceneName = PlayerPrefs.GetString(saveKey + "_SceneName", "");
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (string.IsNullOrEmpty(savedSceneName))
        {
            Debug.LogError("اسم المشهد المحفوظ غير صحيح!");
            ShowMessage("خطأ في البيانات المحفوظة!");
            return;
        }
        
        Debug.Log("المشهد المحفوظ: " + savedSceneName + " - المشهد الحالي: " + currentSceneName);
        
        // إذا كان المشهد المحفوظ مختلف عن المشهد الحالي، قم بتحميله أولاً
        if (savedSceneName != currentSceneName)
        {
            ShowMessage("جاري تحميل المشهد: " + savedSceneName);
            StartCoroutine(LoadSceneAndPosition(savedSceneName));
        }
        else
        {
            // إذا كنا في نفس المشهد، قم بالانتقال للموقع مباشرة
            RestorePlayerPosition();
        }
    }
    
    /// <summary>
    /// تحميل المشهد ثم استعادة الموقع
    /// </summary>
    /// <param name="sceneName">اسم المشهد المراد تحميله</param>
    private IEnumerator LoadSceneAndPosition(string sceneName)
    {
        isLoading = true;
        
        // تحميل المشهد بشكل غير متزامن
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // انتظار حتى يكتمل التحميل
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // انتظار إطار واحد للتأكد من أن كل شيء تم تحميله
        yield return new WaitForEndOfFrame();
        
        // البحث عن اللاعب في المشهد الجديد
        FindPlayerInNewScene();
        
        // استعادة الموقع
        RestorePlayerPosition();
        
        isLoading = false;
        
        ShowMessage("تم الانتقال للمشهد والموقع المحفوظ!");
    }
    
    /// <summary>
    /// البحث عن اللاعب في المشهد الجديد
    /// </summary>
    private void FindPlayerInNewScene()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                Debug.Log("تم العثور على اللاعب في المشهد الجديد");
            }
            else
            {
                Debug.LogError("لم يتم العثور على اللاعب في المشهد الجديد!");
            }
        }
    }
    
    /// <summary>
    /// استعادة موقع ودوران اللاعب
    /// </summary>
    private void RestorePlayerPosition()
    {
        if (playerTransform == null)
        {
            Debug.LogError("مرجع اللاعب غير متوفر!");
            return;
        }
        
        // استعادة الموقع
        float posX = PlayerPrefs.GetFloat(saveKey + "_PosX", 0f);
        float posY = PlayerPrefs.GetFloat(saveKey + "_PosY", 0f);
        float posZ = PlayerPrefs.GetFloat(saveKey + "_PosZ", 0f);
        Vector3 savedPosition = new Vector3(posX, posY, posZ);
        
        // استعادة الدوران
        float rotX = PlayerPrefs.GetFloat(saveKey + "_RotX", 0f);
        float rotY = PlayerPrefs.GetFloat(saveKey + "_RotY", 0f);
        float rotZ = PlayerPrefs.GetFloat(saveKey + "_RotZ", 0f);
        Vector3 savedRotation = new Vector3(rotX, rotY, rotZ);
        
        // تطبيق الموقع والدوران
        playerTransform.position = savedPosition;
        playerTransform.eulerAngles = savedRotation;
        
        Debug.Log("تم استعادة موقع اللاعب: " + savedPosition.ToString());
    }
    
    /// <summary>
    /// التحقق من وجود بيانات محفوظة
    /// </summary>
    /// <returns>true إذا كانت هناك بيانات محفوظة</returns>
    public bool HasSavedData()
    {
        return PlayerPrefs.HasKey(saveKey + "_PosX") && PlayerPrefs.HasKey(saveKey + "_SceneName");
    }
    
    /// <summary>
    /// الحصول على معلومات البيانات المحفوظة
    /// </summary>
    /// <returns>نص يحتوي على معلومات البيانات المحفوظة</returns>
    public string GetSavedDataInfo()
    {
        if (!HasSavedData())
        {
            return "لا توجد بيانات محفوظة";
        }
        
        string sceneName = PlayerPrefs.GetString(saveKey + "_SceneName", "غير معروف");
        float posX = PlayerPrefs.GetFloat(saveKey + "_PosX", 0f);
        float posY = PlayerPrefs.GetFloat(saveKey + "_PosY", 0f);
        float posZ = PlayerPrefs.GetFloat(saveKey + "_PosZ", 0f);
        
        return $"المشهد: {sceneName}\nالموقع: ({posX:F1}, {posY:F1}, {posZ:F1})";
    }
    
    /// <summary>
    /// حذف البيانات المحفوظة
    /// </summary>
    public void ClearSavedData()
    {
        PlayerPrefs.DeleteKey(saveKey + "_SceneName");
        PlayerPrefs.DeleteKey(saveKey + "_PosX");
        PlayerPrefs.DeleteKey(saveKey + "_PosY");
        PlayerPrefs.DeleteKey(saveKey + "_PosZ");
        PlayerPrefs.DeleteKey(saveKey + "_RotX");
        PlayerPrefs.DeleteKey(saveKey + "_RotY");
        PlayerPrefs.DeleteKey(saveKey + "_RotZ");
        PlayerPrefs.Save();
        
        Debug.Log("تم حذف البيانات المحفوظة");
        ShowMessage("تم حذف البيانات!");
    }
    
    /// <summary>
    /// إظهار رسالة للمستخدم
    /// </summary>
    /// <param name="message">الرسالة المراد إظهارها</param>
    private void ShowMessage(string message)
    {
        Debug.Log("رسالة للمستخدم: " + message);
    }
    
    // وظائف للاستخدام مع لوحة المفاتيح
    private void Update()
    {
        // مثال على استخدام لوحة المفاتيح (اختياري)
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayerPosition();
        }
        
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadPlayerPosition();
        }
        
        // عرض معلومات البيانات المحفوظة
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log(GetSavedDataInfo());
        }
    }
}

