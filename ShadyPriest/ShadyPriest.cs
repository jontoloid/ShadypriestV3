using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using ZzukBot.Constants;
using ZzukBot.ExtensionFramework.Classes;
using ZzukBot.Game.Statics;
using ZzukBot.Objects;
using System.Windows.Forms;
using GUI;

[Export(typeof(CustomClass))]
public class ShadyPriest : CustomClass
{
	bool useTouchOfWeakness = true;
	int healthP = 75;
	bool useShadowForm = true;
    bool useMultiDot = true;
    bool useRacial = true;
    bool useVEmb = false;
    bool useSilence = true;

    public MainForm GUI;

    private void DisposeForms()
    {
        foreach (var mainForm in Application.OpenForms.OfType<MainForm>())
        {
            mainForm.Invoke((Action)(() => { mainForm.Close(); }));
        }
        GUI?.Dispose();
       GUI = null;
    }

    public override void Dispose()
    {
        DisposeForms();
    }
    public override bool Load()
    {
        //if (GUI == null) GUI = new MainForm();
        //GUI.Show();
        //GUI.ShowDialog();
        //ShowGui();
        return true;
    }

    public override bool OnBuff() 
        {
        if (ShadySettings.useScrolls)
        {
            UseScrolls();
        }
			if (Spell.Instance.GetSpellRank("Inner Fire") != 0)
			{
			if (!Local.GotAura("Inner Fire"))
			{
				Spell.Instance.Cast("Inner Fire");
				return false;
			}
		}

		if (Spell.Instance.GetSpellRank("Power Word: Fortitude") != 0)
		{
			if (!Local.GotAura("Power Word: Fortitude"))
			{
				Spell.Instance.Cast("Power Word: Fortitude");
				return false;
			}
		}
		if (Spell.Instance.GetSpellRank("Shadowform") != 0 && !Local.GotAura("Shadowform"))
		{
			Spell.Instance.Cast("Shadowform");
			return false;
		}

		return true; 
        }

    private void UseScrolls()
    {// Not checking for whether a buff is already active. I'm lazy.
        for (int i = scrollNames.Length - 1; i >= 0; i--)
            {
            if (Inventory.Instance.GetItemCount(scrollNames[i]) != 0)
            {
                WoWItem scroll = Inventory.Instance.GetItem(scrollNames[i]);
                if (scroll.CanUse())
                {
                    scroll.Use();
                }
            }

            }
        //string scrollToUse = Inventory.Instance.GetLastItem(scrollNames);
        //WoWItem Scroll = Inventory.Instance.GetItem(scrollToUse);
        //Scroll.Use();
    }

    public string[] scrollNames =
        /** A couple of thought regarding the sort of this: 
         * There isn't a feasible scenario in which one would carry 2 different ranks of one scroll.
         * As such, there should be no need for a check to prevent "a more powerful spell is already active" issues.
         * I chose to entirely forego checking aura existences, because I don't care about overwriting existing scroll duration.
        */
    {
        "Scroll of Agility", "Scroll of Agility II", "Scroll of Agility III", "Scroll of Agility IV",
        "Scroll of Strength", "Scroll of Strength II", "Scroll of Strength III", "Scroll of Strength IV",
        "Scroll of Spirit", "Scroll of Spirit II", "Scroll of Spirit III", "Scroll of Spirit IV",
        "Scroll of Intellect", "Scroll of Intellect II", "Scroll of Intellect III", "Scroll of Intellect IV",
        "Scroll of Protection", "Scroll of Protection II", "Scroll of Protection III", "Scroll of Protection IV",
        "Scroll of Stamina", "Scroll of Stamina II", "Scroll of Stamina III", "Scroll of Stamina IV"
    };

    public string[] drinkNames = 
    { //due to the nature of GetLastItem, these should be sorted in an ascending order (LevelReq/Effectiveness)
        "Refreshing Spring Water", "Ice Cold Milk",
        "Melon Juice", "Moonberry Juice",
        "Sweet Nectar", "Morning Glory Dew", "Conjured Purified Water",
        "Conjured Spring Water", "Conjured Mineral Water", "Conjured Sparkling Water",
        "Conjured Crystal Water"
    };

    public string[] foodNames =
    {// Level 0
	    "Bean Soup", "Charred Wolf Meat", "Conjured Muffin", "Leg Meat", "Raw Brilliant Smallfish", "Raw Slitherskin Mackerel", "Red Shiny Apple", "Roasted Boar Meat", "Sickly Looking Fish", "Tough Hunk of Bread", "Tough Jerky", "Darnassian Bleu", 
	    //Level 5
	    "Conjured Bread", "Raw Longjaw Mud Snapper", "Raw Rainbow Fin Albacore", "Tel'Abim Banana", "Versicolor Treat",
	    //Level 10
	    "Raw Sagefish",
	    //Level 15
	    "Conjured Rye", "Snapvine Watermelon", "Dwarven Mild", "Moist Cornbread", "Mutton Chop", "Raw Bristle Whisker Catfish", "Spongy Morel",
	    //Level 25
	    "Conjured Pumpernickel", "Stormwind Brie", "Delicious Cave Mold", "Goldenbark Apple", "Raw Mithril Head Trout", "Mulgore Spice Bread", "Wild Hog Shank",
	    //35
	    "Conjured Sourghdough", "Cured Ham Steak", "Fine Aged Cheddar", "Moon Harvest Pumpkin", "Soft Banana Bread", "Raw Black Truffle",
	    //Level 45
	    "Alterac Swiss", "Conjured Sweet Roll", "Dried King Bolete", "Deep Fried Plantains", "Roasted Quail", "Homemade Cherry Pie"
    };

    public void SelectDrink()
        {
            if (Local.Race == "Night Elf" && Spell.Instance.IsSpellReady("Shadowmeld"))
            {//Shadowmeld drinking
                Spell.Instance.Cast("Shadowmeld");
            }
            string drinkToUse = Inventory.Instance.GetLastItem(drinkNames);
            Local.Drink(drinkToUse);
        }


    public bool MultiDotting()
    {
        if (UnitInfo.Instance.NpcAttackers.Count >= 2 && Local.ManaPercent >= 40 && useMultiDot)
        {
            int properTargetH = UnitInfo.Instance.NpcAttackers.Min(Target => Target.HealthPercent);
            var properTarget = UnitInfo.Instance.NpcAttackers.FirstOrDefault(Target => Target.HealthPercent == properTargetH);
            int newAddH = UnitInfo.Instance.NpcAttackers.Max(Enemy => Enemy.HealthPercent);
            var newAdd = UnitInfo.Instance.NpcAttackers.FirstOrDefault(Enemy => Enemy.HealthPercent == newAddH);
            if (newAdd != null && newAdd.Guid != Target.Guid && !newAdd.GotDebuff("Shadow Word: Pain"))
            {
                if (Spell.Instance.GetSpellRank("Shadow Word: Pain") != 0)
                {
                    if (Spell.Instance.IsSpellReady("Shadow Word: Pain"))
                    {
                        Local.SetTarget(newAdd);
                        Spell.Instance.Cast("Shadow Word: Pain");
                    }
                }
            }
            Local.SetTarget(properTarget);
        }
        return true;
    }

    public void PullPriority()
       {

            if (Spell.Instance.GetSpellRank("Mind Blast") != 0 && Spell.Instance.GetSpellRank("Power Word: Shield") != 0)
            {
                if (Spell.Instance.IsSpellReady("Mind Blast"))
                {
                    if (Spell.Instance.IsSpellReady("Power Word Shield") && !Local.GotDebuff("Weakened Soul"))
                    {
                        Spell.Instance.Cast("Power Word: Shield");
                    }

                    Spell.Instance.Cast("Mind Blast");
                }
            }
            else if (Spell.Instance.GetSpellRank("Shadow Word: Pain") != 0)
            {
                if (Spell.Instance.IsSpellReady("Power Word Shield") && !Local.GotDebuff("Weakened Soul"))
                    {
                        Spell.Instance.Cast("Power Word: Shield");
                    }

                if (!Target.GotDebuff("Shadow Word: Pain"))
                {
                    Spell.Instance.Cast("Shadow Word: Pain");
                }
            }
            else
            {   // to ensure this works from level 1.
                Spell.Instance.Cast("Smite");
            }
        }

    public void SilenceEnemy()
    {   //If this works as intended, the bot will try and silence any casting add, not just hte primary target.
        if (useSilence)
        {
            int properTargetH = UnitInfo.Instance.NpcAttackers.Min(Target => Target.HealthPercent);
            var properTarget = UnitInfo.Instance.NpcAttackers.FirstOrDefault(Target => Target.HealthPercent == properTargetH);
            var castingUnit = UnitInfo.Instance.NpcAttackers.FirstOrDefault(Caster => Caster.Casting != 0 || Caster.Channeling != 0);
            if (castingUnit != null && Spell.Instance.IsSpellReady("Silence"))
            {
                Spell.Instance.StopCasting();
                if (castingUnit.Guid != Target.Guid)
                {
                    Local.SetTarget(castingUnit);
                }
                if (Target.Casting != 0 || Target.Channeling != 0)
                {
                    Spell.Instance.Cast("Silence");
                }
                Local.SetTarget(properTarget);
                return;
            }
        }
        //OLD Version
        //if (Local.ManaPercent >= 75)
        //{   //lookig for spells being cast (by the main target) right now
        //    if (Target.Casting != 0 || Target.Channeling != 0)
        //    {   // checking, if you are specced into Silence
        //        if (Spell.Instance.GetSpellRank("Silence") != 0)
        //        {   //Off CD?
        //            if (Spell.Instance.IsSpellReady("Silence"))
        //            {
        //                Spell.Instance.StopCasting();
        //                Spell.Instance.Cast("Silence");
        //                return;
        //            }
        //        }
        //    }
        //}
    }

    public void MultipleEnemies()
    {
        if (useRacial && UnitInfo.Instance.NpcAttackers.Count >= 2 && Spell.Instance.GetSpellRank("Berserking") != 0 && Spell.Instance.IsSpellReady("Berserking") && Local.Race == "Troll")
        {
            Spell.Instance.Cast("Berserking");
        }
        if (Spell.Instance.GetSpellRank("Psychic Scream") != 0 && UnitInfo.Instance.NpcAttackers.Count >= 2 && Spell.Instance.IsSpellReady("Psychic Scream") && Local.ManaPercent >= 30 && Local.HealthPercent <= 90)
        {
            Spell.Instance.Cast("Psychic Scream");
        }
        if (Spell.Instance.GetSpellRank("Devouring Plague") != 0 && UnitInfo.Instance.NpcAttackers.Count >= 2 && Spell.Instance.IsSpellReady("Devouring Plague") && Local.ManaPercent >= 50 && Local.HealthPercent >= 50)
        {
            Spell.Instance.Cast("Devouring Plague");
        }
        if (Spell.Instance.GetSpellRank("Renew") != 0 && UnitInfo.Instance.NpcAttackers.Count >= 2 && Local.ManaPercent >= 50 && Local.HealthPercent <= 80 && !Local.GotAura("Renew") && !useShadowForm)
        {   //Only doing this pre-shadowform.
            Spell.Instance.CastWait("Renew", 1000);
        }
    }

    public void GotWand()
    {
        if(useShadowForm)
        {
            if(Local.ManaPercent < 10 || Target.HealthPercent <= healthP)
            {
                
                    Spell.Instance.StartWand();
               
            }
        }
    }

    public void NoWand()
    {
        if (Local.ManaPercent > 60)
        {
            Spell.Instance.Cast("Smite");
        }
        else if (Target.IsFleeing)
        {
            Spell.Instance.Cast("Smite");
        }
        else
        {   // TODO: Set Combatdistance to melee for whiteswings
            Spell.Instance.Attack();
        }
    }

    public void OffensiveSpells()
    {
        //PWS 
        if(!Local.GotAura("Power Word: Shield") && !Local.GotDebuff("Weakened Soul") && Spell.Instance.GetSpellRank("Power Word: Shield") != 0 && Spell.Instance.IsSpellReady("Power Word:Shield"))
        {
            Spell.Instance.Cast("Power Word: Shield");
        }

        if(!Local.GotAura("Shadowform") && Spell.Instance.GetSpellRank("Shadowform") != 0)
        {
            Spell.Instance.Cast("Shadowform");
        }

        if(Spell.Instance.GetSpellRank("Shadow Word: Pain") != 0 && Target.HealthPercent >= 5 && Local.ManaPercent >= 10)
        {
            if(!Target.GotDebuff("Shadow Word: Pain"))
            {
                Spell.Instance.Cast("Shadow Word: Pain");
            }
        }

        if(Target.HealthPercent >= 95 && Spell.Instance.GetSpellRank("Mind Blast") != 0 && Spell.Instance.IsSpellReady("Mind Blast") && Local.ManaPercent >= 40)
        {//Mind Blasting additional adds 
            Spell.Instance.Cast("Mind Blast");
        }

        if(useVEmb && Spell.Instance.GetSpellRank("Vampiric Embrace") != 0 && !Target.GotDebuff("Vampiric Embrace"))
        {
            Spell.Instance.Cast("Vampiric Embrace");
        }

        if(Spell.Instance.GetSpellRank("Mind Flay") != 0)
        {
            if(Spell.Instance.IsSpellReady("Mind Flay") && Target.HealthPercent >= healthP && Local.ManaPercent >= 10)
            {
				Spell.Instance.CastWait("Mind Flay", 2500);
            }
        }
    }

    public void DefensiveSpells()
    {
        if((!Local.GotAura("Power Word: Shield") && !Local.GotDebuff("Weakened Soul")) || Local.HealthPercent <= 45)
        {
            if(Spell.Instance.GetSpellRank("Power Word: Shield") != 0)
            {
                if(!Local.GotAura("Power Word: Shield") && !Local.GotDebuff("Weakened Soul"))
                {
                    Spell.Instance.Cast("Power Word: Shield");
                }
            }
        }

        //Healing
		if (Local.HealthPercent <= 45 && Local.ManaPercent >= 40)
        {   //Remove Shadowform to heal
            if(Local.GotAura("Shadowform"))
                {
                    Spell.Instance.Cast("Shadowform");
                }
            if(Spell.Instance.GetSpellRank("Flash Heal") != 0 && Local.ManaPercent >= 55)
            {
                Spell.Instance.CastWait("Flash Heal", 1500);
            }
            else if (Spell.Instance.GetSpellRank("Heal") != 0)
            {
                Spell.Instance.CastWait("Heal", 1500);
            }
            else
            {
                Spell.Instance.CastWait("Lesser Heal", 1500);
            }
        }
    }

    
    public override void OnFight()
    {
        
        bool canWand = Local.IsWandEquipped();
        SilenceEnemy();
        MultipleEnemies();
        DefensiveSpells();
        MultiDotting();
        OffensiveSpells();
        

        //using IsSpellReady as a GCD check like it was done with CanUse in v1. Needed?
        if (canWand && Spell.Instance.IsSpellReady("Shadow Word: Pain"))
        {
			if(Local.Casting == 0 && Local.Channeling == 0)
            GotWand();       
        }

        if (!canWand)
        {
            NoWand();
        }
    }

    public override void OnPull()
    {
		PullPriority();
    }

    public override void OnRest()
    {   //According to v3 api, Drink() Method checks whether drinking is already taking place. No more checks needed?
        if(Local.ManaPercent < ShadySettings.drinkAt || Local.HealthPercent < ShadySettings.eatAt)
        {

        
            if(ShadySettings.autoSelectDrink && Local.ManaPercent < ShadySettings.drinkAt)
            {
                SelectDrink();
            }
            if(!ShadySettings.autoSelectDrink && Local.ManaPercent < ShadySettings.drinkAt && !Local.IsDrinking)
            {
                Local.Drink(ShadySettings.drink);
            }
            if(ShadySettings.autoSelectFood && Local.HealthPercent < ShadySettings.eatAt)
            {
                SelectFood();
            }
            if(ShadySettings.autoSelectFood && Local.HealthPercent < ShadySettings.eatAt && !Local.IsEating)
            {
                Local.Eat(ShadySettings.food);
            }
            return;
        }
    }

    private void SelectFood()
    {
        string foodToUse = Inventory.Instance.GetLastItem(foodNames);
        Local.Eat(foodToUse);
    }

    public override void ShowGui()
    {
        //if (GUI == null) GUI = new MainForm();
        //GUI.Visible = !GUI.Visible;
    }
    public override void Unload() {}

    public WoWUnit Target {get{return ObjectManager.Instance.Target;}}
    public LocalPlayer Local {get{return ObjectManager.Instance.Player;}}
    public List<WoWUnit> Attackers { get { return UnitInfo.Instance.NpcAttackers;} }
    public Spell Spell { get { return Spell.Instance; } }

    public override string Author {get{return "sensgates";}}
    public override string Name {get{return "ShadyPriest";}}
    public override int Version {get{return 1;}}
    public override Enums.ClassId Class {get {return Enums.ClassId.Priest;}}
    public override bool SuppressBotMovement {get{return false;}}
    public override float CombatDistance {get{return 25.0f;}}
}
