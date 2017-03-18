using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.ComponentModel.Composition;
using ZzukBot.Constants;
using ZzukBot.ExtensionFramework.Classes;
using ZzukBot.Game.Statics;
using ZzukBot.Objects;

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

    public override void Dispose() { }
    public override bool Load() { return true;  }

    public override bool OnBuff() 
        {
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


    public string[] drinkNames = {"Refreshing Spring Water", "Ice Cold Milk",
            "Melon Juice", "Moonberry Juice",
            "Sweet Nectar", "Morning Glory Dew", "Conjured Purified Water",
            "Conjured Spring Water", "Conjured Mineral Water", "Conjured Sparkling Water",
            "Conjured Crystal Water" };

    public void SelectDrink()
        {
            if (Local.Race == "Night Elf" && Spell.Instance.IsSpellReady("Shadowmeld"))
            {
            Spell.Instance.Cast("Shadowmeld");
            }
            if (Inventory.Instance.GetItemCount("Morning Glory Dew") != 0)
                Local.Drink(drinkNames[5]);
            else if (Inventory.Instance.GetItemCount("Sweet Nectar") != 0)
                Local.Drink(drinkNames[4]);
            else if (Inventory.Instance.GetItemCount("Moonberry Juice") != 0)
                Local.Drink(drinkNames[3]);
            else if (Inventory.Instance.GetItemCount("Melon Juice") != 0)
                Local.Drink(drinkNames[2]);
            else if (Inventory.Instance.GetItemCount("Ice Cold Milk") != 0)
                Local.Drink(drinkNames[1]);
            else if (Inventory.Instance.GetItemCount("Refreshing Spring Water") != 0)
                Local.Drink(drinkNames[0]);
            else if (Inventory.Instance.GetItemCount("Conjured Purified Water") != 0)
                Local.Drink(drinkNames[6]);
            else if (Inventory.Instance.GetItemCount("Conjured Spring Water") != 0)
                Local.Drink(drinkNames[7]);
            else if (Inventory.Instance.GetItemCount("Conjured Mineral Water") != 0)
                Local.Drink(drinkNames[8]);
            else if (Inventory.Instance.GetItemCount("Conjured Sparkling Water") != 0)
                Local.Drink(drinkNames[9]);
            else if (Inventory.Instance.GetItemCount("Conjured Crystal Water") != 0)
                Local.Drink(drinkNames[10]);        
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
        if(Local.ManaPercent < 20)
            SelectDrink();
            return;
    }

    public override void ShowGui() {}
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
