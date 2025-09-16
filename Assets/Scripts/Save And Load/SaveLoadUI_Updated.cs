using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [Header("UI References")]
    public Button saveButton;      // زر الحفظ
    public Button loadButton;      // زر الاستعادة
    public Button clearButton;     // زر حذف البيانات (اختياري)
    public Button infoButton;      // زر عرض معلومات البيانات المحفوظة (اختياري)
    
    [Header("Save System Reference")]
    public SaveSystem saveSystem;  // مرجع لنظام الحفظ
    
    [Header("Feedback UI (Optional)")]
    public Text feedbackText;      // نص لإظهار الرسائل (اختياري)
    public Text savedDataInfoText; // نص لعرض معلومات البيانات المحفوظة (اختياري)
    public float messageDuration = 3f; // مدة إظهار الرسالة
    
    [Header("Loading UI (Optional)")]
    public GameObject loadingPanel; // لوحة التحميل (اختياري)
    public Text loadingText;       // نص التحميل (اختياري)
    
    private void Start()
    {
        // البحث عن نظام الحفظ تلقائياً إذا لم يتم تحديده
        if (saveSystem == null)
        {
            saveSystem = FindObjectOfType<SaveSystem>();
            if (saveSystem == null)
            {
                Debug.LogError("لم يتم العثور على SaveSystem! تأكد من وجوده في المشهد.");
            }
        }
        
        // ربط الأزرار بالوظائف
        SetupButtons();
        
        // تحديث حالة الأزرار والمعلومات
        RefreshUI();
        
        // إخفاء عناصر UI الاختيارية في البداية
        HideOptionalUI();
    }
    
    /// <summary>
    /// إعداد الأزرار وربطها بالوظائف
    /// </summary>
    private void SetupButtons()
    {
        // ربط زر الحفظ
        if (saveButton != null)
        {
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        else
        {
            Debug.LogWarning("زر الحفظ غير محدد!");
        }
        
        // ربط زر الاستعادة
        if (loadButton != null)
        {
            loadButton.onClick.RemoveAllListeners();
            loadButton.onClick.AddListener(OnLoadButtonClicked);
        }
        else
        {
            Debug.LogWarning("زر الاستعادة غير محدد!");
        }
        
        // ربط زر الحذف (اختياري)
        if (clearButton != null)
        {
            clearButton.onClick.RemoveAllListeners();
            clearButton.onClick.AddListener(OnClearButtonClicked);
        }
        
        // ربط زر المعلومات (اختياري)
        if (infoButton != null)
        {
            infoButton.onClick.RemoveAllListeners();
            infoButton.onClick.AddListener(OnInfoButtonClicked);
        }
    }
    
    /// <summary>
    /// وظيفة زر الحفظ
    /// </summary>
    public void OnSaveButtonClicked()
    {
        if (saveSystem != null)
        {
            saveSystem.SavePlayerPosition();
            ShowFeedbackMessage("تم حفظ الموقع والمشهد بنجاح!");
            
            // تحديث حالة الأزرار والمعلومات
            RefreshUI();
        }
        else
        {
            Debug.LogError("نظام الحفظ غير متوفر!");
            ShowFeedbackMessage("خطأ: نظام الحفظ غير متوفر!");
        }
    }
    
    /// <summary>
    /// وظيفة زر الاستعادة
    /// </summary>
    public void OnLoadButtonClicked()
    {
        if (saveSystem != null)
        {
            if (saveSystem.HasSavedData())
            {
                // إظهار لوحة التحميل إذا كانت متوفرة
                ShowLoadingPanel("جاري تحميل المشهد والموقع...");
                
                saveSystem.LoadPlayerPosition();
                
                // إخفاء لوحة التحميل بعد فترة قصيرة
                Invoke("HideLoadingPanel", 1f);
                
                ShowFeedbackMessage("تم الانتقال للموقع المحفوظ!");
            }
            else
            {
                ShowFeedbackMessage("لا توجد بيانات محفوظة!");
            }
        }
        else
        {
            Debug.LogError("نظام الحفظ غير متوفر!");
            ShowFeedbackMessage("خطأ: نظام الحفظ غير متوفر!");
        }
    }
    
    /// <summary>
    /// وظيفة زر حذف البيانات
    /// </summary>
    public void OnClearButtonClicked()
    {
        if (saveSystem != null)
        {
            saveSystem.ClearSavedData();
            ShowFeedbackMessage("تم حذف جميع البيانات المحفوظة!");
            
            // تحديث حالة الأزرار والمعلومات
            RefreshUI();
        }
        else
        {
            Debug.LogError("نظام الحفظ غير متوفر!");
            ShowFeedbackMessage("خطأ: نظام الحفظ غير متوفر!");
        }
    }
    
    /// <summary>
    /// وظيفة زر عرض المعلومات
    /// </summary>
    public void OnInfoButtonClicked()
    {
        if (saveSystem != null)
        {
            string info = saveSystem.GetSavedDataInfo();
            ShowFeedbackMessage(info);
            
            // عرض المعلومات في نص منفصل إذا كان متوفراً
            if (savedDataInfoText != null)
            {
                savedDataInfoText.text = info;
                savedDataInfoText.gameObject.SetActive(true);
                
                // إخفاء المعلومات بعد فترة
                Invoke("HideSavedDataInfo", messageDuration + 2f);
            }
        }
        else
        {
            ShowFeedbackMessage("نظام الحفظ غير متوفر!");
        }
    }
    
    /// <summary>
    /// تحديث حالة الأزرار والمعلومات
    /// </summary>
    public void RefreshUI()
    {
        if (saveSystem != null)
        {
            bool hasSavedData = saveSystem.HasSavedData();
            
            // تحديث حالة زر الاستعادة
            if (loadButton != null)
            {
                loadButton.interactable = hasSavedData;
                
                // تغيير لون الزر حسب الحالة
                ColorBlock colors = loadButton.colors;
                colors.normalColor = hasSavedData ? Color.white : Color.gray;
                loadButton.colors = colors;
            }
            
            // تحديث حالة زر الحذف
            if (clearButton != null)
            {
                clearButton.interactable = hasSavedData;
            }
            
            // تحديث حالة زر المعلومات
            if (infoButton != null)
            {
                infoButton.interactable = hasSavedData;
            }
            
            // تحديث نص المعلومات إذا كان متوفراً
            if (savedDataInfoText != null && hasSavedData)
            {
                savedDataInfoText.text = saveSystem.GetSavedDataInfo();
            }
        }
    }
    
    /// <summary>
    /// إظهار رسالة للمستخدم
    /// </summary>
    /// <param name="message">الرسالة المراد إظهارها</param>
    private void ShowFeedbackMessage(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            
            // إخفاء الرسالة بعد فترة
            CancelInvoke("HideFeedbackMessage");
            Invoke("HideFeedbackMessage", messageDuration);
        }
        
        // طباعة الرسالة في الكونسول أيضاً
        Debug.Log("رسالة UI: " + message);
    }
    
    /// <summary>
    /// إظهار لوحة التحميل
    /// </summary>
    /// <param name="message">رسالة التحميل</param>
    private void ShowLoadingPanel(string message = "جاري التحميل...")
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
            
            if (loadingText != null)
            {
                loadingText.text = message;
            }
        }
    }
    
    /// <summary>
    /// إخفاء لوحة التحميل
    /// </summary>
    private void HideLoadingPanel()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }
    
    /// <summary>
    /// إخفاء رسالة التغذية الراجعة
    /// </summary>
    private void HideFeedbackMessage()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// إخفاء معلومات البيانات المحفوظة
    /// </summary>
    private void HideSavedDataInfo()
    {
        if (savedDataInfoText != null)
        {
            savedDataInfoText.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// إخفاء عناصر UI الاختيارية في البداية
    /// </summary>
    private void HideOptionalUI()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
        
        if (savedDataInfoText != null)
        {
            savedDataInfoText.gameObject.SetActive(false);
        }
        
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }
    
    /// <summary>
    /// تفعيل أو إلغاء تفعيل جميع الأزرار
    /// </summary>
    /// <param name="enabled">حالة التفعيل</param>
    public void SetButtonsEnabled(bool enabled)
    {
        if (saveButton != null) saveButton.interactable = enabled;
        if (loadButton != null) loadButton.interactable = enabled && (saveSystem != null && saveSystem.HasSavedData());
        if (clearButton != null) clearButton.interactable = enabled && (saveSystem != null && saveSystem.HasSavedData());
        if (infoButton != null) infoButton.interactable = enabled && (saveSystem != null && saveSystem.HasSavedData());
    }
    
    /// <summary>
    /// تحديث النصوص على الأزرار (للدعم متعدد اللغات)
    /// </summary>
    public void UpdateButtonTexts(string saveText, string loadText, string clearText = "", string infoText = "")
    {
        if (saveButton != null)
        {
            Text saveButtonText = saveButton.GetComponentInChildren<Text>();
            if (saveButtonText != null) saveButtonText.text = saveText;
        }
        
        if (loadButton != null)
        {
            Text loadButtonText = loadButton.GetComponentInChildren<Text>();
            if (loadButtonText != null) loadButtonText.text = loadText;
        }
        
        if (clearButton != null && !string.IsNullOrEmpty(clearText))
        {
            Text clearButtonText = clearButton.GetComponentInChildren<Text>();
            if (clearButtonText != null) clearButtonText.text = clearText;
        }
        
        if (infoButton != null && !string.IsNullOrEmpty(infoText))
        {
            Text infoButtonText = infoButton.GetComponentInChildren<Text>();
            if (infoButtonText != null) infoButtonText.text = infoText;
        }
    }
}

