using UnityEngine;

[System.Serializable]
public class DataEnergy {
    private int _maxEnergy;
    private int _currentEnergy;

    public int maxEnergy {
        get { return _maxEnergy; }
        set { _maxEnergy = value;
            currentEnergy = maxEnergy;
        }
    }

    public int currentEnergy {
        get { return _currentEnergy; }
        set { _currentEnergy = Mathf.Clamp(value, 0, maxEnergy); }
    }
}