////Vars

CONST MAX_REL_POINTS = 15
CONST MIN_REL_POINTS = -15

VAR Stas_RelPoints = 0




////Functions

EXTERNAL isGenderBoy()
=== function isGenderBoy() ===
    ~ return false
    
=== function ChangeRelPoints(ref relPoints, value) ===
    ~ relPoints += value
    {
        - relPoints < MIN_REL_POINTS:
            ~ relPoints = MIN_REL_POINTS
        - relPoints > MAX_REL_POINTS:
            ~ relPoints = MAX_REL_POINTS
    }
