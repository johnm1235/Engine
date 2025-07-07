using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MessageStep
{
    [TextArea]
    public string messageText;

    [Tooltip("Evento que se dispara cuando este mensaje se muestra (para activar triggers externos, por ejemplo).")]
    public UnityEvent onMessageShown;

    [Tooltip("Evento que se debe disparar externamente cuando se completa la acción de este paso.")]
    public UnityEvent onActionCompleted;
}
