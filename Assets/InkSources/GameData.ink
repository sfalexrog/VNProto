////Vars

CONST MAX_REL_POINTS = 15
CONST MIN_REL_POINTS = -15

VAR Stas_RelPoints = 0
VAR Leader_RelPoints = 0
VAR Love_RelPoints = 0
VAR Parents_RelPoints = 0
VAR Bumblebee_RelPoints = 0
VAR Lab_RelPoints = 0

////Functions

EXTERNAL isBoy()
=== function isBoy() ===
    ~ return false
    
=== function ChangeRelPoints(ref relPoints, value) ===
    ~ relPoints += value
    {
        - relPoints < MIN_REL_POINTS:
            ~ relPoints = MIN_REL_POINTS
        - relPoints > MAX_REL_POINTS:
            ~ relPoints = MAX_REL_POINTS
    }
